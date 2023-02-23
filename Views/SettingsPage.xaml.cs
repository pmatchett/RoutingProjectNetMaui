namespace RoutingProjectNet;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private void XDimEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        SettingsLabel.Text = $"{e.NewTextValue}";
    }

    private void XDimEntry_Completed(object sender, EventArgs e)
    {
        SettingsLabel.Text = "X entry Completed";
    }

    private void YDimEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        SettingsLabel.Text = "Y entry changed";
    }

    private void YDimEntry_Completed(object sender, EventArgs e)
    {
        SettingsLabel.Text = "Y entry Completed";
    }

    private void ObsSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        SettingsLabel.Text = $"Slider value: {e.NewValue}";
    }
}