using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.IO;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Distances;
using System.Reflection.Emit;
using Accord.Collections;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using Accord.Math;

namespace lvtn
{

    public partial class Form1 : Form
    {
        private List<FileContent> fileContents = new List<FileContent>();

        public Form1()
        {

            InitializeComponent();
            richTextBox2.ReadOnly = true;
        }
        private HashSet<string> GetStopword()
        {
            var data = File.ReadAllText("./stopwords.txt");
            var lines = data.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            var stopword = new HashSet<string>();
            foreach (var word in lines)
            {
                stopword.Add(word.Trim());
            }
            return stopword;
        }
        private string RemoveStopwords(string line)
        {
            var stopword = GetStopword();
            var words = new List<string>();
            line.Trim().Split(' ').ToList().ForEach(word =>
            {
                if (!stopword.Contains(word))
                {
                    words.Add(word);
                }
            });
            return string.Join(" ", words);
        }
        private string TextPreProcessor(string document)
        {
            // Chuyển về chữ in thường
            document = document.ToLower();
            // Xóa các ký tự không cần thiết
            document = Regex.Replace(document, @"[^\s\wáàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệóòỏõọôốồổỗộơớờởỡợíìỉĩịúùủũụưứừửữựýỳỷỹỵđ_]", " ");
            // Xóa khoảng trắng thừa
            document = Regex.Replace(document, @"\s+", " ").Trim();

            return document;
        }
        public void BuildDictionary(int threshold)
        {
            // threshold là ngưỡng mà khi từ đó xuất hiện ít hơn trong từ điển thì loại bỏ nó
            var dictionary = new Dictionary<string, int>(); // khởi tạo đối tượng Dictionary
            var lines = File.ReadAllLines("./5cate.prep");
            foreach (var text in lines)
            {
                // duyệt qua từng văn bản trong tập dữ liệu
                var arrWords = text.Split(' '); // tách các từ trong văn bản thành một mảng
                var words = string.Join(" ", arrWords.Skip(1));
                var wordList = words.Split(' ');
                foreach (var word in wordList)
                {
                    // duyệt qua từng từ trong mảng
                    if (!dictionary.ContainsKey(word))
                    {
                        // nếu từ chưa có trong từ điển
                        dictionary[word] = 1; // thêm từ vào từ điển với số lần xuất hiện ban đầu là 1
                    }
                    else
                    {
                        // nếu từ đã có trong từ điển
                        dictionary[word]++; // tăng số lần xuất hiện của từ lên 1 đơn vị
                    }
                }
            }
            var filteredDict = new Dictionary<string, int>();
            foreach (var entry in dictionary)
            {
                if (entry.Value > threshold)
                {
                    filteredDict[entry.Key] = entry.Value;
                }
            }
            var dictText = filteredDict.Select(entry => $"{entry.Key} {entry.Value}");
            File.WriteAllLines("myDic.txt", dictText);
        }

        public string[] LoadDictionary(string typeofDictionary)
        {

            List<string> dictionary = new List<string>();
            string data = File.ReadAllText($"./Dic/{typeofDictionary}.txt");
            string[] lines = data.Split('\n');
            foreach (string line in lines)
            {
                // tách từ trong dòng và lấy từ ở vị trí thứ 2
                string[] words = line.Split(' '); // Tách chuỗi theo khoảng trắng
                string firstPart = words[0];
                // thực hiện các xử lý với từ ở đây
                dictionary.Add(firstPart);
            }
            return dictionary.ToArray();
        }
        public double[] BagOfWords(string text, string[] dictionary)
        {
            var indexMap = new Dictionary<string, int>(); // Dictionary lưu trữ chỉ số của từ trong dictionary
            for (int i = 0; i < dictionary.Length; i++)
            {
                indexMap[dictionary[i]] = i;
            }

            var vector = new double[dictionary.Length]; // khởi tạo vector với tất cả giá trị ban đầu là 0

            var words = text.Split(' '); // tách các từ trong văn bản thành một mảng

            foreach (var word in words)
            {
                // duyệt qua từng từ trong mảng
                if (indexMap.ContainsKey(word))
                {
                    // nếu từ có trong từ điển
                    var index = indexMap[word]; // lấy chỉ số của từ trong vector
                    vector[index] += 1; // tăng giá trị của phần tử tại chỉ số đó lên 1 đơn vị
                }
            }

            return vector; // trả về vector biểu diễn văn bản
        }
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            var dictionary = LoadDictionary("all");
            List<double[]> input = new List<double[]>();
            List<int> labels = new List<int>();
            string data = File.ReadAllText("./5cate.prep");
            string[] lines = data.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] arrText = lines[i].Split(' ');
                string text = string.Join(" ", arrText.Skip(1));
                input.Add(BagOfWords(text, dictionary));
                labels.Add(EncodedLabel(arrText[0]));
            }
            Console.WriteLine("get ok");
            var teacher = new MulticlassSupportVectorLearning<Linear>()
            {

                Learner = (p) => new LinearDualCoordinateDescent()
                {
                    Loss = Loss.L2
                }
            };

            teacher.ParallelOptions.MaxDegreeOfParallelism = 1;
            Console.WriteLine("started learn");
            MulticlassSupportVectorMachine<Linear> machine = teacher.Learn(input.ToArray(), labels.ToArray());
            Console.WriteLine("end learn");
            Serializer.Save(machine, "MySVMModel.bin");
            MessageBox.Show("Đã huấn luyện xong mô hình SVM", "Thông báo");


        }
        public string Tokenizer(string text)
        {
            var client = new RestClient("https://cubic-fringe-chestnut.glitch.me/");

            // Tạo đối tượng RestRequest với phương thức GET và đường dẫn
            var request = new RestRequest("/tokenizer", Method.Post);
            request.AddJsonBody(new { text });
            // Thực hiện yêu cầu và lấy phản hồi từ API
            var response = client.Execute(request);

            // In ra nội dung của phản hồi
            return response.Content;

        }
        public int EncodedLabel(string label)
        {
            switch (label)
            {
                case "__label__thể_thao":
                    return 0;
                case "__label__giáo_dục":
                    return 1;
                case "__label__kinh_doanh":
                    return 2;
                case "__label__pháp_luật":
                    return 3;
                case "__label__sức_khỏe":
                    return 4;
                default: return -1;
            }
        }
        public string DecodedLabel(int lable)
        {
            switch (lable)
            {
                case 0:
                    return "Thể thao";
                case 1:
                    return "Giáo dục";
                case 2:
                    return "Kinh doanh";
                case 3:
                    return "Pháp luật";
                case 4:
                    return "Sức khoẻ";
                default: return "Chưa xác định";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            var loadedModel = Serializer.Load<MulticlassSupportVectorMachine<Linear>>("MySVMModel.bin");
            var dictionary = LoadDictionary("all");
            string document = TextPreProcessor(richTextBox1.Text);
            document = Tokenizer(document);
            document = RemoveStopwords(document);
            double[] featureVector = BagOfWords(document, dictionary);
            int predicted = loadedModel.Decide(featureVector);

            MessageBox.Show("Đã phân loại xong", "Thông báo");
            txtKq.Text = DecodedLabel(predicted);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABc();
        }
        public float layToaDo(String world)
        {
            int t = 0;
            int c = 0;
            string[] dic1 = LoadDictionary("thethao");
            string[] dic2 = LoadDictionary("giaoduc");
            string[] dic3 = LoadDictionary("kinhdoanh");
            string[] dic4 = LoadDictionary("phapluat");
            string[] dic5 = LoadDictionary("suckhoe");
            if (dic1.Contains(world))
            {
                c++;
                t += 1;
            }
            if (dic2.Contains(world))
            {
                c++;
                t += 2;
            }
            if (dic3.Contains(world))
            {
                c++;
                t += 3;
            }
            if (dic4.Contains(world))
            {
                c++;
                t += 4;
            }
            if (dic5.Contains(world))
            {
                c++;
                t += 5;
            }
            if (c > 0) return (float)t / (float)c;
            else return 0;
        }
        private string[] Top5Word()
        {
            // Tách các từ trong văn bản và đưa vào một mảng các từ
            System.Windows.Forms.Application.UseWaitCursor = true;
            string document = TextPreProcessor(richTextBox1.Text);
            document = Tokenizer(document);
            document = RemoveStopwords(document);
            string[] words = document.Split(' ');
            // Tạo một từ điển để lưu trữ tần suất xuất hiện của từ
            Dictionary<string, int> frequencyDict = new Dictionary<string, int>();

            // Đếm số lần xuất hiện của từ và lưu vào từ điển
            foreach (string word in words)
            {
                if (frequencyDict.ContainsKey(word))
                {
                    frequencyDict[word]++;
                }
                else
                {
                    frequencyDict[word] = 1;
                }
            }

            // Sắp xếp các từ theo thứ tự giảm dần của tần suất xuất hiện
            List<string> mostFrequentWords = frequencyDict.OrderByDescending(x => x.Value)
                                                         .Select(x => x.Key)
                                                         .Take(5)
                                                         .ToList();
            return mostFrequentWords.ToArray();
        }
        double EuclideanDistance(double[] a, int[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += Math.Pow(a[i] - b[i], 2);
            }
            return Math.Sqrt(sum);
        }
        private double[] ABc()
        {
            string[] m = Top5Word();

            List<double> kq = new List<double>();

            for (int i = 0; i < m.Length; i++)
            {
                double toaDo = layToaDo(m[i]);
                kq.Add(toaDo);
            }

            foreach (var item in kq)
            {
                Console.WriteLine(item);
            }
            int[] array1 = Enumerable.Repeat(1, m.Length).ToArray();
            int[] array2 = Enumerable.Repeat(2, m.Length).ToArray();
            int[] array3 = Enumerable.Repeat(3, m.Length).ToArray();
            int[] array4 = Enumerable.Repeat(4, m.Length).ToArray();
            int[] array5 = Enumerable.Repeat(5, m.Length).ToArray();
            int[][] arrays = { array1, array2, array3, array4, array5 };

            double[] distances = new double[arrays.Length];
            for (int i = 0; i < arrays.Length; i++)
            {
                distances[i] = EuclideanDistance(kq.ToArray(), arrays[i]);
                Console.WriteLine(distances[i]);
            }

            double min = distances[0];
            int label = -1;
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < min)
                {
                    min = distances[i];
                    label = i;
                }
            }
            Console.WriteLine(DecodedLabel(label));
            return kq.ToArray();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                // Lấy đường dẫn được chọn
                string path = folderBrowserDialog1.SelectedPath;

                // Thực hiện các thao tác lưu đường dẫn vào đây
                // Ví dụ: hiển thị đường dẫn trong textbox
                textBox1.Text = path;
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        string content = reader.ReadToEnd();
                        FileContent fileContent = new FileContent
                        {
                            Name = file.Name,
                            Content = content
                        };
                        fileContents.Add(fileContent);
                        richTextBox2.Text += file.Name + "\r";
                    }
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var loadedModel = Serializer.Load<MulticlassSupportVectorMachine<Linear>>("MySVMModel.bin");
            var dictionary = LoadDictionary("all");
            richTextBox2.Text += "-----------------------------------------------------------------------------------------" + "\r";
            richTextBox2.Text += "Kết quả sau khi phân loại: " + "\r";
            foreach (FileContent content in fileContents)
            {
                string document = TextPreProcessor(content.Content);
                document = RemoveStopwords(document);
                double[] featureVector = BagOfWords(document, dictionary);
                int predicted = loadedModel.Decide(featureVector);
                richTextBox2.Text += content.Name + " => " + DecodedLabel(predicted) + "\r";
            }
            fileContents.Clear();
            MessageBox.Show("Đã phân loại xong", "Thông báo");
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }

}
