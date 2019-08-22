using System.Windows;
using System.Windows.Controls;

namespace MvvmLightTest.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCTest.xaml
    /// </summary>
    public partial class UCTest : UserControl
    {
        public static readonly DependencyProperty SetTextProperty = DependencyProperty.Register("SetText", typeof(string), typeof(UCTest), new
           PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged)));

        public string SetText
        {
            get { return (string)this.GetValue(SetTextProperty); }
            set { this.SetValue(SetTextProperty, value); }
        }

        private static void OnSetTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCTest UserControl1Control = d as UCTest;
            UserControl1Control.OnSetTextChanged(e);
        }

        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {
            this.tbText.Text = e.NewValue.ToString();
        }

        public UCTest()
        {
            this.InitializeComponent();
        }
    }
}
