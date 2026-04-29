using Microsoft.Maui.Controls;
using System.Management.Automation;
using System.Text;

namespace PowerShellMobile;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnExecuteClicked(object sender, EventArgs e)
    {
        string command = CommandEntry.Text;
        CommandEntry.Text = ""; // limpa a entrada

        if (string.IsNullOrWhiteSpace(command))
        {
            OutputEditor.Text += "> Digite um comando válido.\n";
            return;
        }

        try
        {
            using PowerShell ps = PowerShell.Create();
            ps.AddScript(command);

            var results = ps.Invoke();
            StringBuilder sb = new();

            foreach (var result in results)
            {
                sb.AppendLine(result?.ToString());
            }

            string output = sb.Length > 0 ? sb.ToString() : "Sem saída.";
            OutputEditor.Text += $"> {command}\n{output}\n";
        }
        catch (Exception ex)
        {
            OutputEditor.Text += $"> {command}\nErro: {ex.Message}\n";
        }
    }
}
