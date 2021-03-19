using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using BcdLib.Core;

namespace BcdLib
{
    public enum MinPosition
    {
        LeftBottom = 0,
        RightBottom = 1
    }
    public abstract class BcdForm: ComponentBase,IDisposable
    {
        //private static readonly FieldInfo _innerRenderHandleFieldInfo;
        //private RenderHandle _renderHandle;

        public const string Prefix = "bcd-form";

        static BcdForm()
        {

            //var type = typeof(ComponentBase);
            //_innerRenderHandleFieldInfo = type.GetField("_renderHandle", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected readonly IServiceScope ServiceScope;

        protected BcdForm()
        {
            InitComponent();
            ServiceScope = BcdServices.ServiceProvider.CreateScope();
        }

        protected BcdForm(string name) : this()
        {
            this.Name = name;
        }

        #region form properties

        public string BodyStyle { get; set; }

        /// <summary>
        /// 是否关闭时从DOM中移除
        /// </summary>
        public bool DestroyOnClose { get; set; } = false;

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Name { get; set; } = "Bcd-" + Guid.NewGuid();

        /// <summary>
        /// 允许显示Header
        /// </summary>
        public bool EnableHeader { get; set; } = true;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否显示最小化框
        /// </summary>
        public bool MinimizeBox { get; set; } = true;

        /// <summary>
        /// 是否显示最大化框
        /// </summary>
        public bool MaximizeBox { get; set; } = true;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; private set; }

        /// <summary>
        /// 是否允许拖动
        /// </summary>
        public bool Draggable { get; set; }

        /// <summary>
        /// 仅允许在视窗内拖动
        /// </summary>
        public bool DragInViewport { get; set; }

        #endregion

        protected abstract void InitComponent();

        public bool HasDestroyed { get; set; } = true;

        public MinPosition MinPosition { get; set; } = MinPosition.RightBottom;


       

        internal string GetHeaderCls()
        {
            return Draggable ? "draggable" : "";
        }

        internal string GetStyle()
        {
            return Visible ? "" : "display:none;";
        }


        #region Show

        /// <summary>
        /// 显示 Form 之前的事件，可取消显示
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual Task ShowingAsync(CancelEventArgs e)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 显示 Form
        /// </summary>
        /// <returns></returns>
        public async Task ShowAsync()
        {
            CancelEventArgs eventArgs = new CancelEventArgs(false);
            await ShowingAsync(eventArgs);
            if (eventArgs.Cancel)
            {
                return;
            }

            if (!Visible)
            {
                Visible = true;
                if (HasDestroyed)
                {
                    HasDestroyed = false;
                    await BcdFormContainer.BcdFormContainerInstance.AppendFormAsync(this);
                }
                await InvokeStateHasChangedAsync();
            }
        }

        #endregion

        #region close

        /// <summary>
        /// 关闭 Form 之前，可取消关闭
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual Task ClosingAsync(CancelEventArgs e)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 摧毁之前，从DOM中移除之前
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual Task DestroyingAsync(CancelEventArgs e)
        {
            return Task.CompletedTask;
        }


        /// <summary>
        /// 关闭 Form
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            CancelEventArgs eventArgs = new CancelEventArgs(false);
            await ClosingAsync(eventArgs);

            if (eventArgs.Cancel)
            {
                return;
            }

            if (Visible)
            {
                Visible = false;
                await InvokeStateHasChangedAsync();

                if (DestroyOnClose && !HasDestroyed)
                {
                    HasDestroyed = true;
                    eventArgs = new CancelEventArgs(false);
                    await DestroyingAsync(eventArgs);

                    if (eventArgs.Cancel)
                    {
                        return;
                    }
                    await BcdFormContainer.BcdFormContainerInstance.RemoveFormAsync(this);

                    await InvokeStateHasChangedAsync();
                }
            }
        }

        #endregion

        #region min max

        /// <summary>
        /// form max min or normal
        /// </summary>
        private string FormState { get; set; }

        internal string GetFormState()
        {
            if (IsMin)
            {
                return $"{FormState} min-" + (MinPosition == MinPosition.LeftBottom ? "lb" : "rb");
            }

            return FormState;
        }

        public void Min()
        {
            FormState = $"{Prefix}-min";
        }
        
        public void Max()
        {
            FormState = $"{Prefix}-max";
        }

        public void Normal()
        {
            FormState = "";
        }

        internal void TriggerMaxNormal()
        {
            if (IsMax)
            {
                Normal();
            }
            else 
            {
                Max();
            }
        }

        public bool IsMin => FormState == $"{Prefix}-min";
        public bool IsMax => FormState == $"{Prefix}-max";
        public bool IsNormal => string.IsNullOrWhiteSpace(FormState);

        #endregion


        #region 代理

        /// <summary>
        /// jsRuntime.InvokeVoidAsync 代理
        /// </summary>
        /// <param name="func"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected async Task JsInvokeVoidAsync(string func, params object[] args)
        {
            if (BcdServices.TryGetJsRuntime(out var jsRuntime))
            {
                await jsRuntime.InvokeVoidAsync(func, args);
            }
            else
            {
                // TODO 
            }
        }

        /// <summary>
        /// jsRuntime.InvokeAsync 代理
        /// </summary>
        /// <param name="func"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected async ValueTask<T> JsInvokeAsync<T>(string func, params object[] args)
        {
            if (BcdServices.TryGetJsRuntime(out var jsRuntime))
            {
                return await jsRuntime.InvokeAsync<T>(func, args);
            }
            else
            {
                // TODO
                return await new ValueTask<T>(default(T));
            }
        }

        /// <summary>
        /// StateHasChanged 代理
        /// </summary>
        protected void InvokeStateHasChanged()
        {
            BcdFormContainer.BcdFormContainerInstance.InvokeStateHasChanged();
        }

        /// <summary>
        /// InvokeAsync(StateHasChanged) 代理
        /// </summary>
        /// <returns></returns>
        protected Task InvokeStateHasChangedAsync()
        {
            return BcdFormContainer.BcdFormContainerInstance.InvokeStateHasChangedAsync();
        }

        #endregion
        
        #region dispose

        /// <summary>
        /// 对象是否已经被释放
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// 如果重写，请务必 base.Dispose 调用此函数
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    ServiceScope?.Dispose();
                }
            }
            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BcdForm()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        #endregion

        internal async Task AfterRenderAsync(bool firstRender)
        {
            if (Draggable)
            {
                await JsInvokeVoidAsync(JsInteropConstants.EnableDraggable,
                    $"#{Name} .bcd-form-header .bcd-form-title", $"#{Name} .bcd-form", DragInViewport);
            }

            if (IsMin)
            {
                await JsInvokeVoidAsync(JsInteropConstants.MinResetStyle,$"#{Name}");
            }
            else if (IsMax)
            {
                await JsInvokeVoidAsync(JsInteropConstants.MaxResetStyle, $"#{Name}");
            }

            if (!firstRender)
            {
                if (IsNormal)
                {
                    await JsInvokeVoidAsync(JsInteropConstants.NormalResetStyle, $"#{Name}");
                }

                
            }

            // ReSharper disable once MethodHasAsyncOverload
            AfterRender();

            await AfterRenderAsync();
        }

        /// <summary>
        /// it will trigger in OnAfterRenderAsync
        /// </summary>
        /// <returns></returns>
        protected virtual void AfterRender()
        {
        }
        
        /// <summary>
        /// it will trigger in OnAfterRenderAsync
        /// </summary>
        /// <returns></returns>
        protected virtual Task AfterRenderAsync()
        {
            return Task.CompletedTask;
        }
    }
}
