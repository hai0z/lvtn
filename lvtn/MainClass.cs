using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace lvtn
{
    public class MainClass
    {
        public static string FILE_NAME_DIC = "Dic\\VietAnh.txt";       //duong dan tu dien
        public static string MAIN_PATH = "Dic\\";       //duong dan tu dien
        public static string APPLICATION_NAME = "Thuc tap";               //tên de tai

        //cau truc dung cho mang ket qua gom 1 string va trong so
        public struct Node
        {
            public string w; //tu
            public int t;    //trong so

        }
        //mang cac stop word phai bo di
        public string[] stopWordArr = {    @"\",
                                           @"/",
                                           "?",
                                           "!",
                                           "+",
                                           "-",
                                           "&",
                                           "@",
                                           "(",
                                           ")",
                                           @"/r/n",
                                           ":",
                                           ","
                                           };//,
        //mang cac tu don vo nghia bo di
        public string[] noMeaningArr = {    "là",
                                            "và",
                                            "trong",
                                            "của",
                                            "như",
                                            "những",
                                            "cũng",
                                            "khi",
                                            "với",
                                            "sẽ",
                                            "được",
                                            "ở",
                                            "0",
                                            "1",
                                            "2",
                                            "3",
                                            "4",
                                            "5",
                                            "6",
                                            "7",
                                            "8",
                                            "9"
                                        };//,

        public int MAX_LENGTH = 100000;     //do dai max cua tu dien
        public int MAX_DOC_LENGTH = 1000;   //do dai max cua van ban dau vao
        public int MAX_ARRAY = 5;           //do dai max cua tu dien

        public float[,] arrMain;            //mang trong so
        public Node[] MKQ;                  //mang ket qua va trong so
        public string[] dicAray;            //mang noi dung tu dien chinh
        public string[] listInputFile;      //mang ds file dau vao

        public string note;//thong bao

        public float[,] arrKq;     //mang ket qua tinh duoc
        //ham khoi tao---------------------------------------------------------
        public MainClass()
        {
            //FCM();
            //khoi tao cac thanh phan
            MKQ = new Node[MAX_DOC_LENGTH];
            resetMKQ();
            dicAray = new string[MAX_LENGTH];
            for (int i = 0; i < MAX_LENGTH; i++) dicAray[i] = null;
            initDic();
            //Console.WriteLine(layToaDo("ăn năn"));
            Console.WriteLine("Init MainClass");
        }
        //nap tu dien main-----------------------------------------------------
        public void initDic()
        {
            //doc tu dien ra
            dicAray = new string[MAX_LENGTH];
            int count = 0;

            if (!File.Exists(MainClass.FILE_NAME_DIC))
            {
                //Console.WriteLine("{0} does not exist.", FILE_NAME_DIC);
                MessageBox.Show("Thiếu tập tin từ điển chính!", MainClass.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (StreamReader sr = File.OpenText(MainClass.FILE_NAME_DIC))
            {
                string input;

                while ((input = sr.ReadLine()) != null)
                {
                    dicAray[count] = input.Trim().ToLower();
                    count++;
                    //Console.WriteLine(input);
                }

                //Console.WriteLine("The end of the stream has been reached.");
                sr.Close();
            }
            Console.WriteLine("Read main dictionary ok!");
        }
        //cac ham--------------------------------------------------------------
        public void initMKQ()
        {
            MKQ = new Node[MAX_DOC_LENGTH];
        }
        //cac ham--------------------------------------------------------------
        public void initMKQALL(int c)
        {
            arrMain = new float[c, MAX_ARRAY];
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < MAX_ARRAY; j++)
                {
                    arrMain[i, j] = -1;
                }
            }
        }
        //cap nhat gia tri MKQALL
        public void updateMKQALL(int k)
        {
            for (int i = 0; i < MAX_ARRAY; i++)
            {
                arrMain[k, i] = layToaDo(MKQ[i].w);
            }
        }
        //RESET
        public void resetMKQ()
        {
            for (int i = 0; i < MAX_DOC_LENGTH; i++)
            {
                MKQ[i].t = 0;
                MKQ[i].w = null;
            }
        }
        //dua mot tu vao MKQ---------------------------------------------------
        public void insertMKQ(string w)
        {
            //Console.WriteLine(w);
            for (int i = 0; i < MKQ.Length; i++)
            {
                if (MKQ[i].w == null)
                {
                    //tu chua co trong mang them moi
                    MKQ[i].w = w;
                    MKQ[i].t++;
                    break;
                }
                else if (MKQ[i].w.Equals(w))
                {
                    //tu da co chi tang trong so
                    MKQ[i].t++;
                    break;
                }
            }
        }
        //sap xep MKQ de lay cac trong so cao nhat-----------------------------
        public void sortMKQ()
        {
            Node nt;
            //tang tan suat cho cac tu ghep

            for (int i = 0; i < MKQ.Length; i++)
            {
                if (MKQ[i].w != null)
                    if (MKQ[i].w.IndexOf(" ") > 0) MKQ[i].t = MKQ[i].t * 5 / 2;
            }
            //sap xep theo tan suat
            for (int i = 0; i < MKQ.Length - 1; i++)
            {
                for (int j = 0; j < MKQ.Length; j++)
                {
                    if (MKQ[i].w != null && MKQ[j].w != null)
                    {
                        if (MKQ[i].t > MKQ[j].t)
                        {
                            nt = MKQ[i];
                            MKQ[i] = MKQ[j];
                            MKQ[j] = nt;
                        }
                        else if (MKQ[i].t == MKQ[j].t)
                        {
                            //cung tan suat thi sap xep theo chieu dai hoac abc
                            if (MKQ[i].w.Length > MKQ[j].w.Length)
                            {
                                nt = MKQ[i];
                                MKQ[i] = MKQ[j];
                                MKQ[j] = nt;
                            }
                        }
                    }
                }
            }
        }

        //lay trong so cua tu
        public float layToaDo(string world)
        {
            int t = 0;
            world = world.Trim();
            world = world.ToLower();
            int c = 0;
            if (checkInDic("Dic\\kinhdoanh.txt", world))
            {
                c++;
                t += 1;
            }
            if (checkInDic("Dic\\phapluat.txt", world))
            {
                c++;
                t += 2;
            }
            if (checkInDic("Dic\\thethao.txt", world))
            {
                c++;
                t += 3;
            }
            if (checkInDic("Dic\\vanhoa.txt", world))
            {
                c++;
                t += 4;
            }
            if (c > 0) return (float)t / (float)c;
            //else return 0;
            else return 2.5f;
        }

        public Boolean checkInDic(string dicPath, string w)
        {
            Boolean result = false;
            if (!File.Exists(dicPath))
            {
                //Console.WriteLine("{0} does not exist.", FILE_NAME_DIC);
                MessageBox.Show("Thiếu tập tin từ điển!", MainClass.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
            }


            using (StreamReader sr = File.OpenText(dicPath))
            {
                string input;

                while ((input = sr.ReadLine()) != null)
                {
                    input = input.Trim();
                    input = input.ToLower();
                    if (w.EndsWith(input))
                    {
                        result = true;
                        break;
                    }
                }
                sr.Close();
            }
            return result;
        }
        public float luythua(float f, int m)
        {
            float result = 1;
            for (int i = 0; i < m; i++) result *= f;
            return result;
        }

        public void SVM()
        {


            //mang do dai
            arrKq = new float[listInputFile.Length, 4];
            //mang chua cac dien moc
            int[,] arrType = new int[4, MAX_ARRAY];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < MAX_ARRAY; j++)
                {
                    arrType[i, j] = i + 1;
                    Console.Write(arrType[i, j] + " ");
                }
                Console.Write(" ");
            }
            float total;
            //tinh cac vector 
            for (int i = 0; i < listInputFile.Length; i++)//so luong file dua vao xu ly
            {
                for (int j = 0; j < 4; j++) //so nhom
                {
                    total = 0;
                    for (int k = 0; k < MAX_ARRAY; k++)//so chieu cua 1 vector
                    {
                        total += luythua((arrMain[i, k] - arrType[j, k]), 2);
                    }
                    arrKq[i, j] = (float)Math.Sqrt(total);
                    //Console.Write(arrKq[i, j] + " ");
                }
                //Console.WriteLine(" ");
            }
        }
        //thuat toan cmean------------------------------------------------------------
        public void FCM()
        {
            if (listInputFile == null) return;

            int k = MAX_ARRAY;  //chieu vector
            int c = 4;  //so nhom
            int pt = listInputFile.Length;// ; //so phan tu

            /*//int r  = 0;  //so buoc lap
            int k = 2;// MAX_ARRAY;  //chieu vector
            int c = 2;// 4;  //so nhom
            int pt = 4;// listInputFile.Length;// ; //so phan tu*/

            //float[,] arrMain = new float[pt, k];//mang chua gia tri(toa do) cac van ban
            float[,] v = new float[c, k];
            float[,] d = new float[c, pt]; //chua cac khoanh cach

            float[,] u = new float[c, pt];//
            float[,] u1 = new float[c, pt];//

            int m = 2;
            float e = 0.01f;
            float f1 = 0;
            float f2 = 0;
            Boolean loop = true;

            /*  arrMain = new float[pt, k];
              //gia tri gia de test
              arrMain[0,0]= 1;
              arrMain[0,1]= 3;

              arrMain[1,0]= 1.5f;
              arrMain[1,1]= 3.2f;

              arrMain[2,0]= 1.3f;
              arrMain[2,1]= 2.8f;

              arrMain[3,0]= 3;
              arrMain[3, 1] = 1;
              //thiet lap gia tri ban dau cho ma tran U
              u[0, 0] = 1;
              u[0, 1] = 0;
              u[0, 2] = 0;
              u[0, 3] = 0;

              u[1, 0] = 0;
              u[1, 1] = 1;
              u[1, 2] = 1;
              u[1, 3] = 1;*/

            //khoi tao mang bat dau u babg ham random

            u[0, 0] = 1;
            u[1, 1] = 1;
            u[2, 0] = 1;
            u[3, 0] = 1;
            u[3, 1] = 1;

            //write ra kiem tra
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < pt; j++)
                {
                    Console.Write(u[i, j] + " ");
                }
                Console.WriteLine();
            }


            while (loop)
            {
                //tinh vector V ,công thức (5.23) 
                for (int i = 0; i < c; i++)  //so nhom
                {
                    for (int j = 0; j < k; j++) //chieu vector
                    {
                        f1 = 0;
                        f2 = 0;
                        //tinh tong trong ct 5.23
                        for (int l = 0; l < pt; l++) //so phan tu
                        {
                            f1 += luythua(u[i, l], m) * arrMain[l, j];
                            f2 += luythua(u[i, l], m);
                        }
                        v[i, j] = f1 / f2;
                    }
                }
                //Tiếp theo tính độ đo khoảng cách theo công thức (5.22)
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < pt; j++)
                    {
                        f1 = 0;
                        for (int l = 0; l < k; l++)
                        {
                            f1 += luythua((arrMain[j, l] - v[i, l]), 2);
                        }
                        d[i, j] = (float)Math.Sqrt(f1);
                    }
                }
                //tinh cac gia tri cho U(r+1)
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < pt; j++)
                    {
                        f1 = 0;
                        for (int l = 0; l < c; l++)
                        {
                            if (d[i, j] == d[l, j]) f1 += 1;
                            else f1 += luythua((d[i, j] / d[l, j]), 2);
                        }
                        u1[i, j] = 1 / f1;
                    }
                }
                //write ra kiem tra
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < pt; j++)
                    {
                        Console.Write(u1[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                //danh gia do hoi tu
                f1 = 0;
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < pt; j++)
                    {
                        f2 = Math.Abs(u[i, j] - u1[i, j]);
                        if (f1 < f2) f1 = f2;
                    }
                }
                if (f1 > e)
                {
                    loop = true;
                    u = u1;
                    Console.WriteLine("loop****************");
                }
                else
                {
                    arrKq = u1;
                    loop = false;
                }
            }
        }
    }
}
