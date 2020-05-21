using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsRetro
{
    class W31Skin: Component, ISupportInitialize
    {
        private ContainerControl parentControl;
        public W31Skin()
        {
            //InitializeComponent();
            //((Form)this.Parent).Text = "Windows 3.1 Skin";
        }

        public W31Skin(IContainer container) : this()
        {
            container.Add(this);
        }

        public W31Skin(ContainerControl parentControl) : this()
        {
            this.parentControl = parentControl;
           
        }

        public ContainerControl ContainerControl
        {
            get { return this.parentControl; }
            set
            { this.parentControl = value;
              
            }
        }

        public override ISite Site
        {
            set
            {
                // Runs at design time, ensures designer initializes ContainerControl
                base.Site = value;
                if (value == null) return;
                IDesignerHost service = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (service == null) return;
                IComponent rootComponent = service.RootComponent;
                this.ContainerControl = rootComponent as ContainerControl;
            }
        }

        void ISupportInitialize.BeginInit()
        {
            var form = (Form)this.parentControl;
            //form.Text = "huanito";
            form.FormBorderStyle = FormBorderStyle.None;
        }

        void ISupportInitialize.EndInit()
        {
            var form = (Form)this.parentControl;
            //form.Text = "huanito";
            form.FormBorderStyle = FormBorderStyle.None;
        }
    }
}
