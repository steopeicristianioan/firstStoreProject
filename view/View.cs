using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emag.view
{
    class View
    {
        protected Panel header;
        protected Panel main;
        protected Panel aside;
        protected Panel footer;
        protected int currentUserId;
        protected Form1 form;

        public View(Panel header, Panel main, Panel aside, Panel footer, int id, Form1 form)
        {
            this.header = header;
            this.main = main;
            this.aside = aside;
            this.footer = footer;
            this.currentUserId = id;
            this.form = form;
            this.header.Parent = form;
            this.main.Parent = form;
            this.aside.Parent = form;
            this.footer.Parent = form;
        }

        public Panel Header { get => this.header; }
        public Panel Main { get => this.main; }
        public Panel Aside { get => this.aside; }
        public Panel Footer { get => this.footer; }
        public int UserID { get => this.currentUserId; }
        public Form1 Form { get => this.form; }

        public virtual void loadMain() { }
        protected virtual void loadAside() { }
        protected virtual void loadHeader() { }
        protected virtual void loadFooter() { }
    }
}
