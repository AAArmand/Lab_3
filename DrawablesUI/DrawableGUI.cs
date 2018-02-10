using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DrawablesUI
{
    public class DrawableGUI
    {
        private readonly IDrawable picture; // картинка с методом Draw, которую нужно рисовать
        private readonly Form form; // объект - пользовательский интерфейс приложения
        private Thread thread; // Thread создает и контролирует поток, задает приоритет и возвращает статус

        public DrawableGUI(IDrawable picture)
        {
            this.picture = picture;
            form = new Form();
        }

        public void Refresh()
        {
            form.Invalidate(); // очищает пользовательский интерфейс
        }

        public void Stop()
        {
            form.Invoke((MethodInvoker) (() =>
            {
                form.Close();
                Application.ExitThread();
            })); // закрывает поток и пользовательский интерфейс
        }

        public void Start()
        {
            thread = new Thread(() =>
            {
                form.Paint += (sender, e) =>
                {
                    using (var x = new GraphicsDrawer(e.Graphics))
                    {
                        picture.Draw(x); // рисует фигуру 
                    }
                };

                form.BackColor = Color.White;
                form.Show();
                Application.Run();
            });
            thread.Start();
        }
    }
}