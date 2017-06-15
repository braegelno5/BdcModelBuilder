using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebberCross.BdcModelBuilder.UIControls
{
    /// <summary>
    /// Interaction logic for TreeNode.xaml
    /// </summary>
    public partial class TreeNode : UserControl
    {
        private TreeViewItem _tvi = null;

        public ImageSource Source
        {
            get { return (ImageSource)this.GetValue(ImageSourceProperty); }
            set { this.SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "Source", typeof(ImageSource), typeof(TreeNode));

        public TreeNode()
        {
            InitializeComponent();

        }
    }
}
