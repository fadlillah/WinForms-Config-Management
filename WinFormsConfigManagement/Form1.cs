using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace WinFormsConfigManagement
{
    public partial class Form1 : Form
    {
        public IConfigurationRoot _configuration { get; }
        public Form1()
        {
            InitializeComponent();
            // Build a config object, using env vars and JSON providers.

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetGateInConf();
        }
        private delegate void SetMessage_Method(string message);
        private void SetMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => new SetMessage_Method(SetMessage), new object[] { message });
                return;
            }
            if (textBox1.TextLength > 0)
                textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText($"{message}");
            textBox1.AppendText(Environment.NewLine);
        }
        private GateInConfiguration GetGateInConf()
        {
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            IConfigurationRoot configuration = builder.Build();
            var gateInConfig = new GateInConfiguration();
            IConfigurationSection section = configuration.GetSection("GateInConfiguration");
            section.Bind(gateInConfig);
            return gateInConfig;
        }
        private GateOutConfiguration GetGateOutConf()
        {
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            IConfigurationRoot configuration = builder.Build();
            var gateOutConfig = new GateOutConfiguration();
            IConfigurationSection section = configuration.GetSection("GateOutConfiguration");
            section.Bind(gateOutConfig);
            return gateOutConfig;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var gateInConfig = GetGateInConf();
            string jsonString = JsonSerializer.Serialize(gateInConfig);
            SetMessage(jsonString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var gateOutConfig = GetGateOutConf();
            string jsonString = JsonSerializer.Serialize(gateOutConfig);
            SetMessage(jsonString);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}