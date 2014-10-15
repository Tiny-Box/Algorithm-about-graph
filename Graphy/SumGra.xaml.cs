﻿using System.Windows;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Graphy.Model;
using Graphy.ViewModel;

namespace Graphy
{
    /// <summary>
    /// Description for SumGra.
    /// </summary>
    public partial class SumGra : Window
    {
        /// <summary>
        /// Initializes a new instance of the SumGra class.
        /// </summary>
        public SumGra()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();
        }


    }
}