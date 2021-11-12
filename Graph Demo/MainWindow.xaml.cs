using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graph_Demo {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // Call this to draw it. 
            DrawGraph(new Button(), new RoutedEventArgs());
        }

        /// <summary>
        /// Draw a graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawGraph(object sender, RoutedEventArgs e) {
            // Setting up the bounds of the graph
            const double margin = 10;
            double xmin = margin;
            double xmax = Graph.Width - margin;
            double ymin = margin;
            double ymax = Graph.Height - margin;
            const double step = 10;

            // ##########################################################
            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            
            // Creates the long Horisontal Line
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(Graph.Width, ymax)));
            
            // Adds all the mini lines on the horisontal axis (bottom)
            for (double x = xmin + step;
                x <= Graph.Width - step; x += step) {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }
            
            /* Adds all the lines that were created in the above code to the graph.
             * Stroke thickness = line thickness
             * Stroke = line colour
             * Data = the geometry you are adding (the points that you have created in the above code)
             */
            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;
            Graph.Children.Add(xaxis_path);


            // ##########################################################
            // Creates the Y axis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            
            // Adds the long Vertical Line
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, Graph.Height)));
            
            // Adds the mini lines on the vertical axis (Left)
            for (double y = step; y <= Graph.Height - step; y += step) {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            // Adds them all to the graph.
            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;
            Graph.Children.Add(yaxis_path);


            // ##########################################################
            // This creates the brushes colours
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            Random rand = new Random();

            // This will then go along and create all the colour
            for (int data_set = 0; data_set < 3; data_set++) {
                int last_y = rand.Next((int)ymin, (int)ymax);

                // This is where you add the points to the graph
                // Little bit confusing as it is adding 3 lines at once
                PointCollection points = new PointCollection();
                for (double x = xmin; x <= xmax; x += step) {
                    last_y = rand.Next(last_y - 10, last_y + 10);
                    if (last_y < ymin) last_y = (int)ymin;
                    if (last_y > ymax) last_y = (int)ymax;
                    points.Add(new Point(x, last_y));
                }

                // Adds the lines that connect the points
                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                // Add the line / points
                Graph.Children.Add(polyline);
            }
        }
    }
}
