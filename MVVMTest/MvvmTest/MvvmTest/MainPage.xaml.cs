namespace MvvmTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        MVVMTestViewModel viewModel;
        public MainPage(MVVMTestViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            viewModel.Counter = count;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

        }
    }

}
