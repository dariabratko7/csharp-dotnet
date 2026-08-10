using System;
using System.Diagnostics;
using System.Numerics;     
using System.Threading.Tasks;  
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dz0307
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        private async void btnCalculate_Click(object sender, EventArgs e)
        { 
            if (!int.TryParse(txtInput.Text, out int n) || n < 0)
            {
                MessageBox.Show("Будь ласка, введіть коректне ціле невід'ємне число!",
                                "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
 
            btnCalculate.Enabled = false;                      
            lblStatus.Text = "Обчислення у кількох потоках...";   
            progressBar.Style = ProgressBarStyle.Marquee;    
            txtResult.Clear();                              

            Stopwatch sw = Stopwatch.StartNew();         

            BigInteger result = await Task.Run(() => CalculateFactorialParallel(n));

            sw.Stop(); 

            txtResult.Text = result.ToString();
            lblStatus.Text = $"Завершено за {sw.ElapsedMilliseconds} мс (Використано ядер: {Environment.ProcessorCount})";
            progressBar.Style = ProgressBarStyle.Blocks; 
            btnCalculate.Enabled = true;   
        }

        private BigInteger CalculateFactorialParallel(int n)
        {
            if (n == 0 || n == 1) return 1;

            int threadCount = Environment.ProcessorCount;
            Task<BigInteger>[] tasks = new Task<BigInteger>[threadCount];

            int chunkSize = (n - 1) / threadCount;

            for (int i = 0; i < threadCount; i++)
            {
                int start = 2 + i * chunkSize;
                int end = (i == threadCount - 1) ? n : start + chunkSize - 1;

                tasks[i] = Task.Run(() =>
                {
                    BigInteger subtotal = 1;
                    for (int j = start; j <= end; j++)
                    {
                        subtotal *= j;
                    }
                    return subtotal;
                });
            }

            Task.WaitAll(tasks);

            BigInteger totalResult = 1;
            foreach (var task in tasks)
            {
                totalResult *= task.Result;
            }

            return totalResult;
        }
    }
}