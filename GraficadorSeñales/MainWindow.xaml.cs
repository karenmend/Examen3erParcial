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

namespace GraficadorSeñales
{
   
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double amplitudMaxima;
        Señal señal;
        Señal segundaSeñal;
        Señal señalResultado;

        
        public MainWindow()
        {
       

        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {


            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            

            switch (cbTipoSeñal.SelectedIndex)
            {
                //Senoidal
                case 0:
                    double amplitud = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtAmplitud.Text);
                    double fase = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtFase.Text);
                    double frecuencia = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);

                    break;
                //Rampa
                case 1:
                    señal = new SeñalRampa();
                    break;
                case 2:
                    double alpha = double.Parse(
                         ((ConfiguracionSeñalExponencial)panelConfiguracion.Children[0]).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    break;
                case 3:
                    señal = new SeñalRectangular();
                    break;
                    
                default:
                    señal = null;
                    break;
            }

            //Segunda señal
            switch (cbTipoSeñal_Segunda.SelectedIndex)
            {
                //Senoidal
                case 0:
                    double amplitud = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion_SegundaSeñal.Children[0]).txtAmplitud.Text);
                    double fase = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion_SegundaSeñal.Children[0]).txtFase.Text);
                    double frecuencia = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion_SegundaSeñal.Children[0]).txtFrecuencia.Text);
                    segundaSeñal = new SeñalSenoidal(amplitud, fase, frecuencia);

                    break;
                //Rampa
                case 1:
                    segundaSeñal = new SeñalRampa();
                    break;
                case 2:
                    double alpha = double.Parse(
                         ((ConfiguracionSeñalExponencial)panelConfiguracion_SegundaSeñal.Children[0]).txtAlpha.Text);
                    segundaSeñal = new SeñalExponencial(alpha);
                    break;
                case 3://Rentagular
                    señal = new SeñalRectangular();
                    break;
                default:
                    segundaSeñal = null;
                    break;
            }

            //Establacer los valores que va usar la clase antes de que la llame.
            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;

            //Segunda señal
            segundaSeñal.TiempoInicial = tiempoInicial;
            segundaSeñal.TiempoFinal = tiempoFinal;
            segundaSeñal.FrecuenciaMuestreo = frecuenciaMuestreo;
           

            señal.construirSeñalDigital();
            segundaSeñal.construirSeñalDigital();
            plnGrafica.Points.Clear();
            plnGraficaDos.Points.Clear();



            //Escalar
            double factorEscala = double.Parse(txtfactorEscalaAmplitud.Text);
            if (checkboxEscalaAmplitud.IsChecked == true)
            {
                
                señal.escalar(factorEscala);

            }
            else if (checkboxEscalaAmplitud.IsChecked == false)
            {
                factorEscala = 1;
                señal.escalar(factorEscala);
            }
           
            //Desplazar
            double desplazamientoValor = double.Parse(txtDesplazamientoEnY.Text);
            if(checkboxDesplazamientoY.IsChecked == true)
            {
                señal.desplazar(desplazamientoValor);
            }
           else if (checkboxDesplazamientoY.IsChecked == false)
            {
                desplazamientoValor = 0;
                señal.desplazar(desplazamientoValor);

            }

            //Truncar
            if(checkboxUmbral.IsChecked == true)
            {
                double umbralValor = double.Parse(txtUmbral.Text);
                señal.truncar(umbralValor);
            }
         


            //Señal2
            //Señal senoidal
            double factorEscala2 = double.Parse(txtfactorEscalaAmplitud_SegundaSeñal.Text);

            if (checkboxEscalaAmplitud_SegundaSeñal.IsChecked == true)
            {
                segundaSeñal.escalar(factorEscala2);
            }
            else if (checkboxEscalaAmplitud_SegundaSeñal.IsChecked == false)
            {
                factorEscala2 = 1;
                segundaSeñal.escalar(factorEscala2);
            }

            //Desplazar
            double desplazamientoValor2 = double.Parse(txtDesplazamientoEnY_SegundaSeñal.Text);
            if (checkboxDesplazamientoY_SegundaSeñal.IsChecked == true)
            {
                segundaSeñal.desplazar(desplazamientoValor2);
            }
            else if (checkboxDesplazamientoY_SegundaSeñal.IsChecked == false)
            {
                desplazamientoValor2 = 0;
                segundaSeñal.desplazar(desplazamientoValor2);
            }
            //Truncar
            if (checkboxUmbral_SegundaSeñal.IsChecked == true)
            {
                double umbralValor2 = double.Parse(txtUmbral_SegundaSeñal.Text);
                segundaSeñal.truncar(umbralValor2);
            }

            //Amplitud
            //Amplitud
            señal.actualizarAmplitudMaxima();
            segundaSeñal.actualizarAmplitudMaxima();
            amplitudMaxima = señal.AmplitudMaxima;
            if(segundaSeñal.AmplitudMaxima > amplitudMaxima)
            {
                amplitudMaxima = segundaSeñal.AmplitudMaxima;
            }

            if (señal != null)
            {
                //Recorrer una coleccion o arreglo, solo sirve cuando quieres recorrer todos los elementos.
                //Por cada iteracion se guardara un elemento conforme a la coleccion. (FOREACH)
                //Declarar la variable del tipo de dato que va recorrer
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width,
                    (muestra.Y / amplitudMaxima) * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2))
                    );
                }

            }
            if ( segundaSeñal != null)
            {
                //Recorrer una coleccion o arreglo, solo sirve cuando quieres recorrer todos los elementos.
                //Por cada iteracion se guardara un elemento conforme a la coleccion. (FOREACH)
                //Declarar la variable del tipo de dato que va recorrer
                foreach (Muestra muestra in segundaSeñal.Muestras)
                {
                    plnGraficaDos.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width,
                    (muestra.Y / amplitudMaxima) * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2))
                    );
                }

            }
            lblAmplitudMaximaY.Text = amplitudMaxima.ToString("F");
            lblAmplitudMazimaNegativaY.Text = "-" + amplitudMaxima.ToString("F");
           
            //EJE X
            plnEjex.Points.Clear();
            //Punto de Principio
            plnEjex.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto del fin
            plnEjex.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width, (scrContenedor.Height / 2)));
            //
        
            //EJE Y
            plnEjey.Points.Clear();
            //Punto de Principio
            plnEjey.Points.Add(new Point((0-tiempoInicial) * scrContenedor.Width,  señal.AmplitudMaxima * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //Punto del fin
            plnEjey.Points.Add(new Point((0-tiempoInicial) * scrContenedor.Width, -señal.AmplitudMaxima * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //

           
        }

        private void btnGrficarRampa_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            
            SeñalRampa señal = new SeñalRampa();
            
            double periodoMuestreo = 1 / frecuenciaMuestreo;

            plnGrafica.Points.Clear(); //borra los puntos

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {

                double valorMuestra = señal.evaluar(i);

                señal.Muestras.Add(new Muestra(i, valorMuestra));
            }
               
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width,
                   muestra.Y * (((scrContenedor.Height / 2.0) -30) * -1) + (scrContenedor.Height / 2)));
                }


            
        }

        private void cbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelConfiguracion != null)
            {
                panelConfiguracion.Children.Clear();
                switch (cbTipoSeñal.SelectedIndex)
                {
                    case 0: //Senoidal
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                        break;
                    case 1:
                        break;
                    case 2:
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial());
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }

        private void cbTipoSeñal_Segunda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion_SegundaSeñal.Children.Clear();
            switch (cbTipoSeñal_Segunda.SelectedIndex)
            {
                case 0: //Senoidal
                    panelConfiguracion_SegundaSeñal.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;
                case 1://Rampa
                    break;
                case 2://Exponencial
                    panelConfiguracion_SegundaSeñal.Children.Add(new ConfiguracionSeñalExponencial());
                    break;
                case 3: //Rectangular
                    break;
                default:
                    break;
            }
        }

        private void btnRealizarOperacion_Click(object sender, RoutedEventArgs e)
        {
            señalResultado = null;
            switch (cbTipoOperaacion.SelectedIndex)
            {
                case 0: //Suma
                    señalResultado =
                        Señal.sumar(señal,segundaSeñal);
                        
                    break;
                case 1: //Multiplicacion
                    señalResultado = Señal.multiplicar(señal, segundaSeñal);
                    break;
                case 2:
                    señalResultado = Señal.convolucionar(señal, segundaSeñal);
                    break;
                case 3:
                    señalResultado = Señal.correlacion(señal, segundaSeñal);
                    break;
                default:
                    break;

            }

            señalResultado.actualizarAmplitudMaxima();
            plnGraficaResultado.Points.Clear();

            lblAmplitudMaximaY_Resultado.Text = señalResultado.AmplitudMaxima.ToString("F");
            lblAmplitudMazimaNegativaY_Resultado.Text = "-" + señalResultado.AmplitudMaxima.ToString("F");
            if (señalResultado != null)
            {
                
                foreach (Muestra muestra in señalResultado.Muestras)
                {
                    plnGraficaResultado.Points.Add(new Point((muestra.X - señalResultado.TiempoInicial) * scrContenedor_Resultado.Width,
                    (muestra.Y / señalResultado.AmplitudMaxima) * (((scrContenedor_Resultado.Height / 2.0) - 30) * -1) + (scrContenedor_Resultado.Height / 2))
                    );
                }

            }
          
         

            //EJE X
            plnEjexResultado.Points.Clear();
            //Punto de Principio
            plnEjexResultado.Points.Add(new Point(0, (scrContenedor_Resultado.Height / 2)));
            //Punto del fin
            plnEjexResultado.Points.Add(new Point((señalResultado.TiempoFinal - señalResultado.TiempoInicial) * scrContenedor_Resultado.Width, (scrContenedor_Resultado.Height / 2)));
            //

            //EJE Y
            plnEjeyResultado.Points.Clear();
            //Punto de Principio
            plnEjeyResultado.Points.Add(new Point((0 - señalResultado.TiempoInicial) * scrContenedor_Resultado.Width, señalResultado.AmplitudMaxima * (((scrContenedor_Resultado.Height / 2.0) - 30) * -1) + (scrContenedor_Resultado.Height / 2)));
            //Punto del fin
            plnEjeyResultado.Points.Add(new Point((0 - señalResultado.TiempoInicial) * scrContenedor_Resultado.Width, - señalResultado.AmplitudMaxima * (((scrContenedor_Resultado.Height / 2.0) - 30) * -1) + (scrContenedor_Resultado.Height / 2)));
            //

        }
    }
}
