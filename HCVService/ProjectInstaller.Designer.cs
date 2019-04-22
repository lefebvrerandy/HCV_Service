namespace HCVService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HCVServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.HCVServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // HCVServiceProcessInstaller
            // 
            this.HCVServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.HCVServiceProcessInstaller.Password = null;
            this.HCVServiceProcessInstaller.Username = null;
            // 
            // HCVServiceInstaller
            // 
            this.HCVServiceInstaller.ServiceName = "HCVService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.HCVServiceProcessInstaller,
            this.HCVServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller HCVServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller HCVServiceInstaller;
    }
}