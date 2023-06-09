﻿using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace GUI.ArticleGUIs
{
    /// <summary>
    /// Interaktionslogik für AddArticle.xaml
    /// </summary>
    public partial class AddArticle : Page
    {
       
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public AddArticle()
        {
            InitializeComponent();                 
        }


        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            AllowEuroPrice(e);
        }

        private void TargetStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void ActualStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        public static void AllowEuroPrice(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result)|| e.Text=="." || e.Text==","))
            {
                e.Handled = true;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ArticlePage articlePage = new ArticlePage();
            NavigationService.Navigate(articlePage);
        }

        private void CreateArticle(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!Name.Text.IsNullOrEmpty() && !Price.Text.IsNullOrEmpty() && !TargetStock.Text.IsNullOrEmpty() && !ActualStock.Text.IsNullOrEmpty())
                {
                    if (Price.Text.Contains(".") || Price.Text.Contains(","))
                    {
                        string[] string_remove = { ".", "," };
                        string euroPrice = Price.Text;

                        foreach (string c in string_remove)
                        {
                            euroPrice = euroPrice.Replace(c, "");
                        }

                        int centPrice = Int32.Parse(euroPrice);

                        Article article = new Article(Name.Text, centPrice, Int32.Parse(TargetStock.Text), Int32.Parse(ActualStock.Text));
                        admin.AddArticle(article);

                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);

                    }
                    else
                    {
                        Article article = new Article(Name.Text, Int32.Parse(Price.Text), Int32.Parse(TargetStock.Text), Int32.Parse(ActualStock.Text));
                        admin.AddArticle(article);

                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);                     
                    }
                }
                else
                {
                    WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
                }
            }
        }

    }
}
