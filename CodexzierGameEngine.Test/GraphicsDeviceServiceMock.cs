// using Microsoft.Xna.Framework.Graphics;
//
// namespace CodexzierGameEngine.Test;
//
// public class GraphicsDeviceServiceMock : IGraphicsDeviceService
// {
//     GraphicsDevice _GraphicsDevice;
//     Form HiddenForm;
//
//     public GraphicsDeviceServiceMock()
//     {
//         this.HiddenForm = new Form()
//         {
//             Visible = false,
//             ShowInTaskbar = false
//         };
//
//         var Parameters = new PresentationParameters()
//         {
//             BackBufferWidth = 1280,
//             BackBufferHeight = 720,
//             DeviceWindowHandle = this.HiddenForm.Handle,
//             PresentationInterval = PresentInterval.Immediate,
//             IsFullScreen = false
//         };
//
//         this._GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, Parameters);
//     }
//
//     public GraphicsDevice GraphicsDevice
//     {
//         get { return this._GraphicsDevice; }
//     }
//
//     public event EventHandler<EventArgs> DeviceCreated;
//     public event EventHandler<EventArgs> DeviceDisposing;
//     public event EventHandler<EventArgs> DeviceReset;
//     public event EventHandler<EventArgs> DeviceResetting;
//
//     public void Release()
//     {            
//         this._GraphicsDevice.Dispose();
//         this._GraphicsDevice = null;
//
//         this.HiddenForm.Close();            
//         this.HiddenForm.Dispose();
//         this.HiddenForm = null;
//     }
// }