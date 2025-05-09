﻿using assigment_4_IMDB.Models;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace assigment_4_IMDB.Views
{
    /// <summary>
    /// Interaction logic for MoviesDetailsView.xaml
    /// </summary>
    public partial class MoviesDetailsView : UserControl
    {
        public Title Movie { get; }
        public MoviesDetailsView(Title movie)
        {
            InitializeComponent();
            Movie = movie;
            DataContext = Movie;
        }
    }
}
