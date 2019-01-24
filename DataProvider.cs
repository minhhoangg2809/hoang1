 public  class DataProvider
    {
        private static DataProvider _ins;

        public static DataProvider Ins
        {
            get {
                if (_ins==null)
                {
                     _ins = new DataProvider();
                }
                return _ins; 
            }
            set {
                _ins = value; 
            }
        }

        public QuanLy_KhoHangEntities DB { get;  set; }
        public DataProvider() {
            DB = new QuanLy_KhoHangEntities();
        }
    }