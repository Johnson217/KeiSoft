using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeiSoft
{
    public partial class Psychological : Form
    {
        public Bitmap Ar { get; set; } = Properties.Resources.ar2;
        public Bitmap CurrentPhoto { get; set; }
        public string CurrentQuestion { get; set; }
        public Dictionary<Bitmap, string> PhtotoProperty { get; set; }
        public List<string> QuestionProperty { get; set; }
        public StringBuilder Builder { get; set; }
        public bool Flag { get; set; }
        public bool Finish { get; set; } = false;
        public bool CurrentTest { get; set; }
        public int CurrentSeed { get; set; }
        public Psychological(int seed)
        {
            InitializeComponent();
            CurrentSeed = seed;
            init(seed, true);
            

            this.radioButton1.Select();

        }

        private void init(int seed, bool test)
        {
            CurrentTest = test;
            this.label2.Text = string.Empty;
            PhtotoProperty = new Dictionary<Bitmap, string>();
            QuestionProperty = new List<string>();
            Builder = new StringBuilder();
            PhtotoProperty = PhtotoEntity.GetBitmaps(seed);
            QuestionProperty = QuestionEntity.GetQuestion(seed);
            if (!test)
            {
                string pd = string.Empty;
                pd = ConfigurationManager.AppSettings[$"PhotoDescription{seed}"];

                if (string.IsNullOrEmpty(pd))
                {
                    this.label1.Text = "";
                }
                else
                {
                    this.label1.Text = pd.ToString().Trim();
                }
            }
            else
            {
                this.label1.Text = "";
            }
            SetAr();
            change();
        }

        private Task change()
        {
            var th = new Thread
              (
                  delegate ()
                  {
                      Thread.Sleep(3000);
                      execute();
                  }
              );
            th.IsBackground = true;
            th.Start();
            return Task.CompletedTask;
        }

        private void execute()
        {
            work(RamdonPhtoto(), RamdonQuestion());
        }

        private void work(Bitmap photo, string question)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            ChangeImage(photo);
            this.label2.Text = "Q:" + question;
        }

        private void SetAr()
        {
            ChangeImage(Ar);
        }

        private Bitmap RamdonPhtoto()
        {
            Random rm = new Random();
            var index = rm.Next(0, 2);
            CurrentPhoto = (PhtotoProperty.Keys.ToArray())[index];
            return CurrentPhoto;
        }

        /// <summary>
        /// 改变图片
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="millisecondsTimeOut">切换图片间隔时间</param>
        private void ChangeImage(Bitmap img)
        {

            this.pb.Image = img;
            this.pb.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private string RamdonQuestion()
        {
            removeQuestion(this.label2.Text);
            if (QuestionProperty.Count == 1)
            {
                return QuestionProperty[0];
            }

            Random rm = new Random();
            var index = rm.Next(0, QuestionProperty.Count - 1);
            CurrentQuestion = QuestionProperty[index];
            return CurrentQuestion;
        }

        private void removeQuestion(string castQuestion)
        {
            QuestionProperty.Remove(castQuestion.Replace("Q:", ""));
        }

        /// <summary>
        /// 等待计时器
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        private bool sleep(int second)
        {


            Stopwatch sp = new Stopwatch();
            sp.Start();
            while (sp.IsRunning)
            {
                if (sp.ElapsedMilliseconds >= 2000)
                {
                    sp.Stop();
                }
            }
            return true;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (Finish)
            {
                MessageBox.Show("别做了");
                return;
            }

            if (CurrentPhoto == null || CurrentQuestion == null)
            {
                return;
            }
            SetAr();
            if (QuestionProperty.Count == 1)
            {
                SetResult();

                if (CurrentTest)
                {
                    MessageBox.Show("测试做完了");
                    init(CurrentSeed, false);
                    Thread.Sleep(2000);
                    return;
                }
                else
                {

                }

                MessageBox.Show(Builder.ToString());
                QuickLog.LogLine(Builder.ToString());
                Finish = true;
            }
            else
            {
                if (string.IsNullOrEmpty(getRaidoSelect()))
                {
                    MessageBox.Show("请选择！");
                }
                else
                {
                    SetResult();
                    change();
                }
            }
        }

        private void SetResult()
        {
            var answser = getRaidoSelect();
            Builder.AppendLine($"{this.label2.Text}|A:{answser}|Phtot:{PhtotoProperty[CurrentPhoto]}");
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.button1_Click(sender, e);
            }

            if (e.KeyChar == (char)Keys.Left)
            {
                if (string.IsNullOrEmpty(getRaidoSelect()))
                {
                    this.radioButton1.Select();
                }
            }
        }


        private string getRaidoSelect()
        {
            foreach (Control rd in this.groupBox2.Controls)
            {

                if (rd is RadioButton)
                {
                    if (((RadioButton)rd).Checked)
                    {
                        return rd.Text;
                    }
                }
            }

            return string.Empty;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
