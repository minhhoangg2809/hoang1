 #region Filter
        private void tb_filter_TextChanged(object sender, RoutedEventArgs e)
        {
            CollectionView viewfilter = (CollectionView)CollectionViewSource.GetDefaultView(Litsview_find.ItemsSource);
            viewfilter.Filter = FilterUser;
            CollectionViewSource.GetDefaultView(Litsview_find.ItemsSource).Refresh();
        }

        private bool FilterUser(object item)
        {
            if (String.IsNullOrEmpty(tb_filter.Text))
            {
                return true;
            }
            else
            {
                return ((_Class)item).property.IndexOf(tb_filter.Text, StringComparison.OrdinalIgnoreCase) >= 0||
                // 
            }
        }
        #endregion