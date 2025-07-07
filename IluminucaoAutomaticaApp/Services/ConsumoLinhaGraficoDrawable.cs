using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IluminucaoAutomaticaApp.Services
{
    public class SerieConsumo
    {
        public string Nome { get; set; } = "";
        public List<PontoConsumo> Pontos { get; set; } = new();
        public Func<double, string> FormatarLabelX { get; set; } = x => x.ToString();
    }
    public class PontoConsumo
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class ConsumoLinhaGraficoDrawable : IDrawable
    {
        public List<SerieConsumo> Series { get; set; } = new();

        public string EixoXLabel { get; set; } = "";
        public string EixoYLabel { get; set; } = "Consumo (KWh)";

        private readonly Color[] _cores = new Color[]
        {
            Colors.CadetBlue,
            Colors.OrangeRed,
            Colors.MediumPurple,
            Colors.SeaGreen,
            Colors.Goldenrod
        };

        //public void Draw(ICanvas canvas, RectF dirtyRect)
        //{
        //    if (Series == null || Series.Count == 0 || Series.All(s => s.Pontos.Count == 0))
        //        return;

        //    float marginLeft = 50, marginBottom = 40, marginTop = 30, marginRight = 20;
        //    float width = dirtyRect.Width - marginLeft - marginRight;
        //    float height = dirtyRect.Height - marginTop - marginBottom;

        //    var allX = Series.SelectMany(s => s.Pontos.Select(p => p.X)).Distinct().OrderBy(x => x).ToList();
        //    var allY = Series.SelectMany(s => s.Pontos.Select(p => p.Y)).ToList();

        //    if (allX.Count == 0 || allY.Count == 0)
        //        return;

        //    double minX = allX.Min(), maxX = allX.Max();
        //    double minY = 0, maxY = Math.Max(allY.Max(), 0.001);

        //    canvas.StrokeColor = Colors.DarkSlateGray;
        //    canvas.StrokeSize = 1;
        //    canvas.DrawLine(marginLeft, marginTop + height, marginLeft + width, marginTop + height); // eixo X
        //    canvas.DrawLine(marginLeft, marginTop, marginLeft, marginTop + height); // eixo Y

        //    canvas.FontColor = Colors.Black;
        //    canvas.FontSize = 14;
        //    canvas.DrawString(EixoYLabel, 5, marginTop + height / 2, HorizontalAlignment.Left);
        //    canvas.DrawString(EixoXLabel, marginLeft + width / 2, dirtyRect.Height - 10, HorizontalAlignment.Center);

        //    int xTicks = Math.Min(7, allX.Count);
        //    for (int i = 0; i < xTicks; i++)
        //    {
        //        int idx = (int)Math.Round(i * (allX.Count - 1) / (double)(xTicks - 1));
        //        idx = Math.Max(0, Math.Min(idx, allX.Count - 1));
        //        double xVal = allX[idx];
        //        float x = marginLeft + (float)((xVal - minX) / (maxX - minX) * width);
        //        canvas.StrokeColor = Colors.LightGray;
        //        canvas.DrawLine(x, marginTop, x, marginTop + height);
        //        canvas.FontColor = Colors.Black;
        //        canvas.FontSize = 11;
        //        canvas.DrawString(Series[0].FormatarLabelX(xVal), x, marginTop + height + 5, HorizontalAlignment.Center);
        //    }

        //    int yTicks = 5;
        //    for (int i = 0; i <= yTicks; i++)
        //    {
        //        double yVal = minY + (maxY - minY) * i / yTicks;
        //        float y = marginTop + height - (float)((yVal - minY) / (maxY - minY) * height);
        //        canvas.StrokeColor = Colors.LightGray;
        //        canvas.DrawLine(marginLeft, y, marginLeft + width, y);
        //        canvas.FontColor = Colors.Black;
        //        canvas.FontSize = 11;
        //        canvas.DrawString(yVal.ToString("0.###"), marginLeft - 5, y, HorizontalAlignment.Right);
        //    }

        //    // Desenha as linhas das séries
        //    for (int s = 0; s < Series.Count; s++)
        //    {
        //        var serie = Series[s];
        //        if (serie.Pontos.Count < 2) continue;
        //        var cor = _cores[s % _cores.Length];
        //        canvas.StrokeColor = cor;
        //        canvas.StrokeSize = 2;

        //        for (int i = 1; i < serie.Pontos.Count; i++)
        //        {
        //            float x1 = marginLeft + (float)((serie.Pontos[i - 1].X - minX) / (maxX - minX) * width);
        //            float y1 = marginTop + height - (float)((serie.Pontos[i - 1].Y - minY) / (maxY - minY) * height);
        //            float x2 = marginLeft + (float)((serie.Pontos[i].X - minX) / (maxX - minX) * width);
        //            float y2 = marginTop + height - (float)((serie.Pontos[i].Y - minY) / (maxY - minY) * height);
        //            canvas.DrawLine(x1, y1, x2, y2);
        //        }

        //        // Pontos
        //        foreach (var p in serie.Pontos)
        //        {
        //            float x = marginLeft + (float)((p.X - minX) / (maxX - minX) * width);
        //            float y = marginTop + height - (float)((p.Y - minY) / (maxY - minY) * height);
        //            canvas.FillColor = cor;
        //            canvas.FillCircle(x, y, 4);
        //        }
        //    }

        //    // Legenda
        //    float legendaX = marginLeft + 10, legendY = marginTop + 5;
        //    for (int s = 0; s < Series.Count; s++)
        //    {
        //        var cor = _cores[s % _cores.Length];
        //        canvas.FillColor = cor;
        //        canvas.FillRectangle(legendaX, legendY + s * 20, 15, 8);
        //        canvas.FontColor = Colors.Black;
        //        canvas.FontSize = 12;
        //        canvas.DrawString(Series[s].Nome, legendaX + 20, legendY + s * 20 + 4, HorizontalAlignment.Left);
        //    }
        //}

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Series == null || Series.Count == 0 || Series.All(s => s.Pontos.Count == 0))
                return;

            float marginLeft = 60, marginBottom = 40, marginTop = 30, marginRight = 20;
            float width = dirtyRect.Width - marginLeft - marginRight;
            float height = dirtyRect.Height - marginTop - marginBottom;

            var allX = Series.SelectMany(s => s.Pontos.Select(p => p.X)).Distinct().OrderBy(x => x).ToList();
            var allY = Series.SelectMany(s => s.Pontos.Select(p => p.Y)).ToList();

            if (allX.Count == 0 || allY.Count == 0)
                return;

            double minX = allX.Min(), maxX = allX.Max();
            double minY = 0, maxY = Math.Max(allY.Max(), 0.001);

            double rangeX = maxX - minX;
            if (rangeX == 0) rangeX = 1;
            double rangeY = maxY - minY;
            if (rangeY == 0) rangeY = 1;

            // Eixos
            canvas.StrokeColor = Colors.DarkSlateGray;
            canvas.StrokeSize = 1;
            canvas.DrawLine(marginLeft, marginTop + height, marginLeft + width, marginTop + height); // eixo X
            canvas.DrawLine(marginLeft, marginTop, marginLeft, marginTop + height); // eixo Y

            // Label do eixo Y (vertical)
            canvas.SaveState();
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 14;
            // Rotaciona o canvas para desenhar o texto na vertical
            canvas.Translate(15, marginTop + height / 2);
            canvas.Rotate(-90);
            canvas.DrawString(EixoYLabel, 0, 0, HorizontalAlignment.Center);
            canvas.RestoreState();

            // Label do eixo X (centralizado)
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 14;
            canvas.DrawString(EixoXLabel, marginLeft + width / 2, dirtyRect.Height - 10, HorizontalAlignment.Center);

            // Ticks e labels do eixo X
            int xTicks = Math.Min(7, allX.Count);
            if (xTicks > 1)
            {
                for (int i = 0; i < xTicks; i++)
                {
                    int idx = (int)Math.Round(i * (allX.Count - 1) / (double)(xTicks - 1));
                    idx = Math.Max(0, Math.Min(idx, allX.Count - 1));
                    double xVal = allX[idx];
                    float x = marginLeft + (float)((xVal - minX) / rangeX * width);
                    canvas.StrokeColor = Colors.LightGray;
                    canvas.DrawLine(x, marginTop, x, marginTop + height);
                    canvas.FontColor = Colors.Black;
                    canvas.FontSize = 11;
                    // Label do eixo X
                    canvas.DrawString(Series[0].FormatarLabelX(xVal), x, marginTop + height + 5, HorizontalAlignment.Center);
                }
            }
            else if (xTicks == 1)
            {
                double xVal = allX[0];
                float x = marginLeft + width / 2;
                canvas.StrokeColor = Colors.LightGray;
                canvas.DrawLine(x, marginTop, x, marginTop + height);
                canvas.FontColor = Colors.Black;
                canvas.FontSize = 11;
                canvas.DrawString(Series[0].FormatarLabelX(xVal), x, marginTop + height + 5, HorizontalAlignment.Center);
            }

            // Ticks e labels do eixo Y
            int yTicks = 5;
            for (int i = 0; i <= yTicks; i++)
            {
                double yVal = minY + (maxY - minY) * i / yTicks;
                float y = marginTop + height - (float)((yVal - minY) / rangeY * height);
                canvas.StrokeColor = Colors.LightGray;
                canvas.DrawLine(marginLeft, y, marginLeft + width, y);
                canvas.FontColor = Colors.Black;
                canvas.FontSize = 11;
                canvas.DrawString(yVal.ToString("0.###"), marginLeft - 8, y, HorizontalAlignment.Right);
            }

            // Desenha as linhas das séries
            for (int s = 0; s < Series.Count; s++)
            {
                var serie = Series[s];
                if (serie.Pontos.Count < 2) continue;
                var cor = _cores[s % _cores.Length];
                canvas.StrokeColor = cor;
                canvas.StrokeSize = 2;

                for (int i = 1; i < serie.Pontos.Count; i++)
                {
                    float x1 = marginLeft + (float)((serie.Pontos[i - 1].X - minX) / rangeX * width);
                    float y1 = marginTop + height - (float)((serie.Pontos[i - 1].Y - minY) / rangeY * height);
                    float x2 = marginLeft + (float)((serie.Pontos[i].X - minX) / rangeX * width);
                    float y2 = marginTop + height - (float)((serie.Pontos[i].Y - minY) / rangeY * height);
                    canvas.DrawLine(x1, y1, x2, y2);
                }

                // Pontos
                foreach (var p in serie.Pontos)
                {
                    float x = marginLeft + (float)((p.X - minX) / rangeX * width);
                    float y = marginTop + height - (float)((p.Y - minY) / rangeY * height);
                    canvas.FillColor = cor;
                    canvas.FillCircle(x, y, 4);
                }
            }

            // Legenda
            float legendaX = marginLeft + 10, legendY = marginTop + 5;
            for (int s = 0; s < Series.Count; s++)
            {
                var cor = _cores[s % _cores.Length];
                canvas.FillColor = cor;
                canvas.FillRectangle(legendaX, legendY + s * 20, 15, 8);
                canvas.FontColor = Colors.Black;
                canvas.FontSize = 12;
                canvas.DrawString(Series[s].Nome, legendaX + 20, legendY + s * 20 + 4, HorizontalAlignment.Left);
            }
        }
    }
}
