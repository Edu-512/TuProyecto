using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TuProyecto
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, List<double>> calificacionesPorEstudiante;

        public MainPage()
        {
            InitializeComponent();
            calificacionesPorEstudiante = new Dictionary<string, List<double>>();
        }

        private async void AgregarCalificacion_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(estudianteEntry.Text) && !string.IsNullOrWhiteSpace(valorEntry.Text))
            {
                string estudiante = estudianteEntry.Text;
                double valor;

                if (double.TryParse(valorEntry.Text, out valor))
                {
                    if (!calificacionesPorEstudiante.ContainsKey(estudiante))
                    {
                        calificacionesPorEstudiante.Add(estudiante, new List<double>());
                        estudiantePicker.ItemsSource = calificacionesPorEstudiante.Keys.ToList();
                    }

                    calificacionesPorEstudiante[estudiante].Add(valor);

                    estudianteEntry.Text = string.Empty;
                    valorEntry.Text = string.Empty;

                    await DisplayAlert("Éxito", "Calificación agregada", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Ingrese un valor numérico válido", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Ingrese el nombre del estudiante y el valor de la calificación", "OK");
            }
        }

        private void EstudiantePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estudianteSeleccionado = estudiantePicker.SelectedItem as string;

            if (!string.IsNullOrEmpty(estudianteSeleccionado))
            {
                if (calificacionesPorEstudiante.ContainsKey(estudianteSeleccionado))
                {
                    List<double> calificacionesEstudiante = calificacionesPorEstudiante[estudianteSeleccionado];

                    if (calificacionesEstudiante.Count > 0)
                    {
                        double suma = calificacionesEstudiante.Sum();
                        double promedio = suma / calificacionesEstudiante.Count;

                        resultadoLabel.Text = "Promedio de " + estudianteSeleccionado + ": " + promedio.ToString("N2");
                    }
                    else
                    {
                        resultadoLabel.Text = "No hay calificaciones para " + estudianteSeleccionado;
                    }
                }
                else
                {
                    resultadoLabel.Text = "No hay calificaciones para " + estudianteSeleccionado;
                }
            }
        }

        private void CalcularPromedio_Clicked(object sender, EventArgs e)
        {
            if (calificacionesPorEstudiante.Count > 0)
            {
                double sumaTotal = 0;
                int contadorTotal = 0;

                foreach (var calificacionesEstudiante in calificacionesPorEstudiante.Values)
                {
                    sumaTotal += calificacionesEstudiante.Sum();
                    contadorTotal += calificacionesEstudiante.Count;
                }

                if (contadorTotal > 0)
                {
                    double promedioTotal = sumaTotal / contadorTotal;
                    resultadoLabel.Text = "Promedio Total: " + promedioTotal.ToString("N2");
                }
                else
                {
                    resultadoLabel.Text = "No hay calificaciones para calcular el promedio";
                }
            }
            else
            {
                resultadoLabel.Text = "No hay calificaciones para calcular el promedio";
            }
        }
    }
}