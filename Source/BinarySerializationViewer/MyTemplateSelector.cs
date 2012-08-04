/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 04/12/2010
 * Time: 10:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace BinarySerializationViewer
{
    /// <summary>
    /// Converts generic types to keys to help work around WPF DataTemplate limitations.
    /// </summary>
    public class MyTemplateSelector : DataTemplateSelector
    {
        public MyTemplateSelector()
        {
        }
        
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            string key = null;
            if (item.GetType().FullName.Contains("ValueNode") && item.GetType().IsGenericType)
            {
                key = "valueNode";    
            }

            if (key == null)
            {
                if (item.GetType().IsGenericType)
                {
                    var typeArgs = item.GetType().GetGenericArguments();
                    
                    key = item.GetType().Name;
                    foreach (var typeArg in typeArgs)
                    {
                        
                    }
                }
            }
            
            if (key != null)
            {
                var element = (System.Windows.FrameworkElement)container;
                return element.FindResource(key) as DataTemplate;
            }
            else
            {
                return base.SelectTemplate(item, container);
            }
        }
    }
}
