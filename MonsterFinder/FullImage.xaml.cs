﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonsterFinder
{
    /// <summary>
    /// Interaction logic for FullImage.xaml
    /// </summary>
    public partial class FullImage : Window
    {
        public string Url { get; set; }

        public FullImage()
        {
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
