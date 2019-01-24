public class CustomGridPanel: Grid
    {
        private enum Action
        {
            Open,
            Close
        }
 
        #region Variables
        private const double DEFAULT_SPEED = 5;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private double speed;
        private double exspectedMinWidth;
        private double exspectedMaxWidth;
 
        #endregion
 
        public static event PropertyChangedCallback IsOpenChanged;
 
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
 
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(CustomGridPanel),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender, IsOpenChangedAction));
 
        private static void IsOpenChangedAction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (IsOpenChanged != null)
            {
                IsOpenChanged.Invoke(d, e);
            }
        }
 
        public CustomGridPanel()
        {
            IsOpenChanged += (sender, e) =>
            {
                InvokeAction(IsOpen ? Action.Open : Action.Close);
            };
        }
 
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
 
            // Init variable
            exspectedMaxWidth = Width;
            exspectedMinWidth = MinWidth;
        }
 
        private void InvokeAction(Action action)
        {
            if (IsLoaded)
            {
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 3);
 
                // Reset speed
                speed = DEFAULT_SPEED;
 
                if (action == Action.Open)
                {
                    // Open animation
                    OpenAnimation();
                }
                else
                {
                    // Close animation
                    CloseAnimation();
                }
 
                // Start timer
                dispatcherTimer.Start();
            }
            else if (action == Action.Close)
            {
                Visibility = Visibility.Collapsed;
                Width = exspectedMinWidth;
            }
            else if (action == Action.Open)
            {
                Visibility = Visibility.Visible;
                Width = exspectedMaxWidth;
            }
        }
 
        private void CloseAnimation()
        {
            dispatcherTimer.Tick += (sender, e) =>
            {
                if (Width > exspectedMinWidth)
                {
                    speed += 1;
 
                    if (Width - exspectedMinWidth < speed) { speed = Width - exspectedMinWidth; } Width -= speed; } else { // Stop timer dispatcherTimer.Stop(); Visibility = Visibility.Collapsed; } }; } private void OpenAnimation() { Visibility = Visibility.Visible; dispatcherTimer.Tick += (sender, e) =>
            {
                if (Width < exspectedMaxWidth)
                {
                    speed += 1;
 
                    if (exspectedMaxWidth - Width < speed)
                    {
                        speed = exspectedMaxWidth - Width;
                    }
 
                    Width += speed;
                }
                else
                {
                    // Stop timer
                    dispatcherTimer.Stop();
                }
            };
        }
    }