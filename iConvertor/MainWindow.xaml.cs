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

namespace iConvertor
{
    /// <summary>
    /// Test exercise
    /// </summary>
    /// 
    interface iConverter
    {
        bool SetInputValue(string text);
        string GetResult();
    }


    abstract class Convertable : iConverter
    {
        public double input, result;
        public bool isDot;
        public bool SetInputValue(string text)
        {
            isDot = text.Contains('.');
            text = text.Replace('.', ',');
            return double.TryParse(text, out input);


        }

        abstract protected void Process();
        public string GetResult()
        {
            Process();
            string text = result.ToString();
            if (isDot)
            {
                text = text.Replace(',', '.');
            }
            return text;
        }
    }
    class ConvertableDegrees : Convertable
    {

        private enumDegrees _from, _to;
        public enum enumDegrees { Fahrenheit, Celsius };

        public bool SetConvertType(enumDegrees from, enumDegrees to)
        {
            _from = from;
            _to = to;

            if (from == to)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        protected override void Process()
        {
            if (_from == enumDegrees.Celsius)
            {
                if (input != 0)
                {
                    result = input * 1.8 + 32;
                }
                else
                {
                    return;
                }

            }
            else if (_from == enumDegrees.Fahrenheit)
            {
                if (input != 0)
                {
                    result = (input - 32) / 1.8;
                }
                else
                {
                    return;
                }

            }
            else
            {
                MessageBox.Show("Please indicate only degrees");
            }
        }
    }
    class ConvertableLengths : Convertable
    {
        //private double baseValue = 1.00;
        private enumLength _from, _to;
        public enum enumLength { km, m, sm, inch, yard, foot };

        public bool SetConvertType(enumLength from, enumLength to)
        {
            _from = from;
            _to = to;


            if (from == to)
            {
                MessageBox.Show("PLEASE CHANGE THE CONVERT MEASURE");
                return false;
            }
            else
            {
                return true;
            }

        }


        protected override void Process()
        {
            //double tempValue;
            switch (_from)
            {
                case enumLength.km:

                    result = input * 1000;

                    break;
                case enumLength.m:
                    result = input;
                    break;
                case enumLength.sm:

                    result = input / 100;
                    break;
                case enumLength.inch:
                    //form
                    result = input / 0.0254;

                    break;
                case enumLength.foot:
                    //
                    result = input / 0.3048;
                    break;
                case enumLength.yard:
                    //
                    result = input / 0.9144;
                    break;
                default:
                    break;
            }


            switch (_to)
            {
                case enumLength.km:

                    result = result / 1000;
                    break;
                case enumLength.m:
                    result = result;
                    break;
                case enumLength.sm:
                    result = result * 100;
                    break;
                case enumLength.inch:
                    //form
                    result = result * 0.0254;

                    break;
                case enumLength.foot:
                    //
                    result = result * 0.3048;
                    break;
                case enumLength.yard:
                    //
                    result = result * 0.9144;
                    break;
                default:
                    break;
            }

        }
    }
    class ConvertableWeight : Convertable
    {
        //private double baseValue = 1.00;
        private enumWeight _from, _to;
        public enum enumWeight { kg, g, ounce, pound, tone };

        public bool SetConvertType(enumWeight from, enumWeight to)
        {
            _from = from;
            _to = to;

            if (from == to)
            {
                MessageBox.Show("PLEASE CHANGE THE CONVERT MEASURE");
                return false;
            }
            else
            {
                return true;
            }

        }

        protected override void Process()
        {
            //double tempValue;
            switch (_from)
            {
                case enumWeight.g:

                    result = input;

                
                    break;
                case enumWeight.kg:
                    result = input * 1000;
                    break;
                case enumWeight.ounce:
                    //
                    result = input * 28.35;
                    break;
                case enumWeight.pound:
                    //form
                    result = input * 453.59;

                    break;
                case enumWeight.tone:
                    //
                    result = input * 1000000;
                    break;

                default:
                    break;
            }


            switch (_to)
            {
                case enumWeight.g:

                    result = result;
                    break;
                case enumWeight.kg:
                    result = result / 1000;
                    break;
                case enumWeight.ounce:
                    result = result / 28.35;
                    break;
                case enumWeight.pound:
                    //form
                    result = result / 453.59;

                    break;
                case enumWeight.tone:
                    //
                    result = result / 1000000;
                    break;

                default:
                    break;
            }

        }
    }
    class ConvertableTime : Convertable
    {
        //private double baseValue = 1.00;
        private enumTime _from, _to;
        public enum enumTime { minutes, seconds, milliseconds };

        public bool SetConvertType(enumTime from, enumTime to)
        {
            _from = from;
            _to = to;

            if (from == to)
            {
                MessageBox.Show("PLEASE CHANGE THE CONVERT MEASURE");
                return false;
            }
            else
            {
                return true;
            }

        }

        protected override void Process()
        {
            //double tempValue;
            switch (_from)
            {
                case enumTime.milliseconds:

                    result = input / 60000;

                    break;
                case enumTime.minutes:
                    result = input;
                    break;
                case enumTime.seconds:

                    result = input / 60;
                    break;


                default:
                    break;
            }


            switch (_to)
            {
                case enumTime.milliseconds:

                    result = result * 60000;

                    break;
                case enumTime.minutes:
                    result = result;
                    break;
                case enumTime.seconds:
                    //
                    result = result * 60;
                    break;

                default:
                    break;
            }

        }
    }
    public partial class MainWindow : Window
    {
        enum Serials { Degrees = 0, Lengths = 1, Weight = 2, Time = 3 };
        enum Measures
        {
            Fahrenheit = 0, Celsius = 1, km = 2, m = 3, sm = 4,
            inch = 5, yard = 6, foot = 7, kg = 8, g = 9, ounce = 10,
            pound = 11, tone = 12, minutes = 13, seconds = 14, milliseconds = 15
        };


        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Serials fromSerial;
            fromSerial = (Serials)comboBox1.SelectedIndex;
            Measures fromMeasure, toMeasure;
            fromMeasure = (Measures)comboBox2.SelectedIndex;
            toMeasure = (Measures)comboBox3.SelectedIndex;



            string inputData = textBox1.Text;
            string userResult;

            if (inputData == "")
            {
                MessageBox.Show("Please enter a value into INPUT");
                return;
            }

            switch (fromSerial)
            {
                case Serials.Degrees:
                    ConvertableDegrees objDegrees = new ConvertableDegrees();

                    objDegrees.SetInputValue(inputData);
                    if (comboBox2.SelectedIndex == comboBox3.SelectedIndex)
                    {
                        MessageBox.Show("PLEASE SELECT OTHER DEGREE MEASURE");
                        return;
                    }
                    if ((comboBox2.SelectedIndex != 0) && (comboBox2.SelectedIndex != 1))
                    {
                        MessageBox.Show("PLEASE SELECT DEGREE MEASURE");
                        return;
                    }
                    if ((comboBox3.SelectedIndex != 0) && (comboBox3.SelectedIndex != 1))
                    {
                        MessageBox.Show("PLEASE SELECT DEGREE MEASURE");
                        return;
                    }


                    if (fromMeasure == Measures.Celsius)
                    {

                        objDegrees.SetConvertType(ConvertableDegrees.enumDegrees.Celsius, ConvertableDegrees.enumDegrees.Fahrenheit);
                        userResult = objDegrees.GetResult();

                    }
                    else if (fromMeasure == Measures.Fahrenheit)
                    {
                        objDegrees.SetConvertType(ConvertableDegrees.enumDegrees.Fahrenheit, ConvertableDegrees.enumDegrees.Celsius);
                        userResult = objDegrees.GetResult();

                    }


                    else
                    {
                        return;
                    }
                    textBox2.Text = userResult;


                    break;
                case Serials.Lengths:
                    ConvertableLengths objLength = new ConvertableLengths();
                    objLength.SetInputValue(inputData);

                    if (comboBox2.SelectedIndex == comboBox3.SelectedIndex)
                    {
                        MessageBox.Show("PLEASE SELECT DIFFERENET LENGTH MEASURES");
                        return;
                    }
                    if ((comboBox2.SelectedIndex != 2) && (comboBox2.SelectedIndex != 3) && (comboBox2.SelectedIndex != 4) && (comboBox2.SelectedIndex != 5) && (comboBox2.SelectedIndex != 6) && (comboBox2.SelectedIndex != 7))
                    {
                        MessageBox.Show("PLEASE SELECT LENGTH MEASURES");
                        return;
                    }
                    if ((comboBox3.SelectedIndex != 2) && (comboBox3.SelectedIndex != 3) && (comboBox3.SelectedIndex != 4) && (comboBox3.SelectedIndex != 5) && (comboBox3.SelectedIndex != 6) && (comboBox3.SelectedIndex != 7))
                    {
                        MessageBox.Show("PLEASE SELECT LENGTH MEASURE IN THE RESULT CONVERTATION FIELD");
                        return;
                    }


                    switch (fromMeasure)
                    {
                        case Measures.Fahrenheit:
                            MessageBox.Show("PLEASE SELECT LENGTH MEASURE");

                            break;
                        case Measures.Celsius:
                            MessageBox.Show("PLEASE SELECT LENGTH MEASURE");

                            break;
                        case Measures.km:

                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.km, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.sm:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.km, ConvertableLengths.enumLength.sm);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.inch:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.km, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.yard:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.km, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.foot:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.km, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.kg:

                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case Measures.m:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.m, ConvertableLengths.enumLength.km);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.m, ConvertableLengths.enumLength.sm);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.inch:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.m, ConvertableLengths.enumLength.inch);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.yard:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.m, ConvertableLengths.enumLength.yard);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.foot:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.m, ConvertableLengths.enumLength.foot);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case Measures.sm:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.sm, ConvertableLengths.enumLength.km);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.m:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.sm, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.sm, ConvertableLengths.enumLength.inch);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.yard:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.sm, ConvertableLengths.enumLength.yard);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.foot:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.sm, ConvertableLengths.enumLength.foot);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.inch:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.inch, ConvertableLengths.enumLength.km);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.m:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.inch, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.sm:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.inch, ConvertableLengths.enumLength.sm);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.inch, ConvertableLengths.enumLength.yard);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.foot:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.inch, ConvertableLengths.enumLength.foot);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.yard:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.yard, ConvertableLengths.enumLength.km);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.m:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.yard, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.sm:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.yard, ConvertableLengths.enumLength.sm);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.inch:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.yard, ConvertableLengths.enumLength.inch);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.yard, ConvertableLengths.enumLength.foot);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.foot:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.foot, ConvertableLengths.enumLength.km);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.m:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.foot, ConvertableLengths.enumLength.m);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.sm:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.foot, ConvertableLengths.enumLength.sm);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.inch:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.foot, ConvertableLengths.enumLength.inch);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.yard:
                                    objLength.SetConvertType(ConvertableLengths.enumLength.foot, ConvertableLengths.enumLength.yard);
                                    userResult = objLength.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.kg:
                            break;
                        case Measures.g:
                            break;
                        case Measures.ounce:
                            break;
                        case Measures.pound:
                            break;
                        case Measures.tone:
                            break;
                        case Measures.minutes:
                            break;
                        case Measures.seconds:
                            break;
                        case Measures.milliseconds:
                            break;
                        default:
                            break;
                    }



                    break;
                case Serials.Weight:
                    ConvertableWeight objWeight = new ConvertableWeight();
                    objWeight.SetInputValue(inputData);

                    if (comboBox2.SelectedIndex == comboBox3.SelectedIndex)
                    {
                        MessageBox.Show("PLEASE SELECT DIFFERENT WEIGHT MEASURES");
                        return;
                    }
                    if ((comboBox2.SelectedIndex != 8) && (comboBox2.SelectedIndex != 9) && (comboBox2.SelectedIndex != 10) && (comboBox2.SelectedIndex != 11) && (comboBox2.SelectedIndex != 12))
                    {
                        MessageBox.Show("PLEASE SELECT WEIGHT MEASURE IN THE INPUT FIELD");
                        return;
                    }
                    if ((comboBox3.SelectedIndex != 8) && (comboBox3.SelectedIndex != 9) && (comboBox3.SelectedIndex != 10) && (comboBox3.SelectedIndex != 11) && (comboBox3.SelectedIndex != 12))
                    {
                        MessageBox.Show("PLEASE SELECT WEIGHT MEASURE IN THE RESULT CONVERTATION FIELD");
                        return;
                    }


                    switch (fromMeasure)
                    {
                        case Measures.Fahrenheit:
                            break;
                        case Measures.Celsius:
                            break;
                        case Measures.km:
                            break;
                        case Measures.m:
                            break;
                        case Measures.sm:
                            break;
                        case Measures.inch:
                            break;
                        case Measures.yard:
                            break;
                        case Measures.foot:
                            break;
                        case Measures.kg:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.kg, ConvertableWeight.enumWeight.g);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.ounce:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.kg, ConvertableWeight.enumWeight.ounce);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.pound:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.kg, ConvertableWeight.enumWeight.pound);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.tone:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.kg, ConvertableWeight.enumWeight.tone);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.g:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.g, ConvertableWeight.enumWeight.kg);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.g, ConvertableWeight.enumWeight.ounce);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.pound:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.g, ConvertableWeight.enumWeight.pound);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.tone:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.g, ConvertableWeight.enumWeight.tone);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.ounce:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.ounce, ConvertableWeight.enumWeight.kg);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.g:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.ounce, ConvertableWeight.enumWeight.g);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.ounce, ConvertableWeight.enumWeight.pound);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.tone:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.ounce, ConvertableWeight.enumWeight.tone);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case Measures.pound:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.pound, ConvertableWeight.enumWeight.kg);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.g:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.pound, ConvertableWeight.enumWeight.g);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.ounce:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.pound, ConvertableWeight.enumWeight.ounce);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.pound, ConvertableWeight.enumWeight.tone);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.tone:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.tone, ConvertableWeight.enumWeight.kg);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.g:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.tone, ConvertableWeight.enumWeight.g);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.ounce:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.tone, ConvertableWeight.enumWeight.ounce);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.pound:
                                    objWeight.SetConvertType(ConvertableWeight.enumWeight.tone, ConvertableWeight.enumWeight.pound);
                                    userResult = objWeight.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.minutes:
                            break;
                        case Measures.seconds:
                            break;
                        case Measures.milliseconds:
                            break;
                        default:
                            break;
                    }

                    break;
                case Serials.Time:
                    ConvertableTime objTime = new ConvertableTime();
                    objTime.SetInputValue(inputData);
                    if (comboBox2.SelectedIndex == comboBox3.SelectedIndex)
                    {
                        MessageBox.Show("PLEASE SELECT DIFFERENT TIME MEASURES");
                        return;
                    }
                    if ((comboBox2.SelectedIndex != 13) && (comboBox2.SelectedIndex != 14) && (comboBox2.SelectedIndex != 15))
                    {
                        MessageBox.Show("PLEASE SELECT TIME MEASURE IN THE INPUT FIELD");
                        return;
                    }
                    if ((comboBox3.SelectedIndex != 13) && (comboBox3.SelectedIndex != 14) && (comboBox3.SelectedIndex != 15))
                    {
                        MessageBox.Show("PLEASE SELECT TIME MEASURE IN THE RESULT CONVERTATION FIELD");
                        return;
                    }

                    switch (fromMeasure)
                    {
                        case Measures.Fahrenheit:
                            break;
                        case Measures.Celsius:
                            break;
                        case Measures.km:
                            break;
                        case Measures.m:
                            break;
                        case Measures.sm:
                            break;
                        case Measures.inch:
                            break;
                        case Measures.yard:
                            break;
                        case Measures.foot:
                            break;
                        case Measures.kg:
                            break;
                        case Measures.g:
                            break;
                        case Measures.ounce:
                            break;
                        case Measures.pound:
                            break;
                        case Measures.tone:
                            break;
                        case Measures.minutes:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:

                                    break;
                                case Measures.seconds:
                                    objTime.SetConvertType(ConvertableTime.enumTime.minutes, ConvertableTime.enumTime.seconds);
                                    userResult = objTime.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.milliseconds:
                                    objTime.SetConvertType(ConvertableTime.enumTime.minutes, ConvertableTime.enumTime.milliseconds);
                                    userResult = objTime.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.seconds:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    objTime.SetConvertType(ConvertableTime.enumTime.seconds, ConvertableTime.enumTime.minutes);
                                    userResult = objTime.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.seconds:
                                    break;
                                case Measures.milliseconds:
                                    objTime.SetConvertType(ConvertableTime.enumTime.seconds, ConvertableTime.enumTime.milliseconds);
                                    userResult = objTime.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Measures.milliseconds:
                            switch (toMeasure)
                            {
                                case Measures.Fahrenheit:
                                    break;
                                case Measures.Celsius:
                                    break;
                                case Measures.km:
                                    break;
                                case Measures.m:
                                    break;
                                case Measures.sm:
                                    break;
                                case Measures.inch:
                                    break;
                                case Measures.yard:
                                    break;
                                case Measures.foot:
                                    break;
                                case Measures.kg:
                                    break;
                                case Measures.g:
                                    break;
                                case Measures.ounce:
                                    break;
                                case Measures.pound:
                                    break;
                                case Measures.tone:
                                    break;
                                case Measures.minutes:
                                    objTime.SetConvertType(ConvertableTime.enumTime.milliseconds, ConvertableTime.enumTime.minutes);
                                    userResult = objTime.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.seconds:
                                    objTime.SetConvertType(ConvertableTime.enumTime.milliseconds, ConvertableTime.enumTime.seconds);
                                    userResult = objTime.GetResult();
                                    textBox2.Text = userResult;
                                    break;
                                case Measures.milliseconds:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }


    }
}
