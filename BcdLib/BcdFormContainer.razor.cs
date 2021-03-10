using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BcdLib
{
    public partial class BcdFormContainer
    {
        internal static BcdFormContainer BcdFormContainerInstance { get; private set; }

        private readonly HashSet<BcdForm> _forms;
        private readonly Dictionary<BcdForm, RenderFragment> _form2Compontents;
        private readonly FieldInfo _innerRenderFragmentFieldInfo;

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        internal IJSRuntime BcdJsRuntime { get; set; }

        public BcdFormContainer()
        {
            if (BcdFormContainerInstance == null)
            {
                // TODO
            }

            var type = typeof(ComponentBase);
            _innerRenderFragmentFieldInfo = type.GetField("_renderFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            BcdFormContainerInstance = this;
            _forms = new HashSet<BcdForm>();
            _form2Compontents = new Dictionary<BcdForm, RenderFragment>();
        }

        /// <summary>
        /// 从 DOM 中删除 Form
        /// </summary>
        /// <param name="bForm"></param>
        /// <returns></returns>
        internal async Task RemoveFormAsync(BcdForm bForm)
        {
            if (_forms.Contains(bForm))
            {
                _forms.Remove(bForm);
                _form2Compontents.Remove(bForm);
                await InvokeAsync(StateHasChanged);
            }
        }

        /// <summary>
        /// 向DOM中添加 BForm
        /// </summary>
        /// <param name="form"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        internal async Task AppendFormAsync(BcdForm form, Func<Task> callback = null)
        {
            if (!_forms.Contains(form))
            {
                _forms.Add(form);
                RenderFragment value = _innerRenderFragmentFieldInfo.GetValue(form) as RenderFragment;
                _form2Compontents.Add(form, value);
                await InvokeAsync(StateHasChanged);
                if (callback != null)
                {
                    _onAfterRender = callback;
                }
            }
        }

        static async Task CloseFormAsync(BcdForm form)
        {
            await form.CloseAsync();
        }

        private Func<Task> _onAfterRender;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_onAfterRender != null)
            {
                await _onAfterRender.Invoke();
                _onAfterRender = null;
            }

            foreach (var form in _forms)
            {
                await form.AfterRenderAsync(firstRender);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        internal void InvokeStateHasChanged()
        {
            StateHasChanged();
        }

        internal Task InvokeStateHasChangedAsync()
        {
            return InvokeAsync(StateHasChanged);
        }

        internal void InvokeStateHasChanged(Func<Task> onAfterRender)
        {
            _onAfterRender = onAfterRender;
            InvokeStateHasChanged();
        }

        internal Task InvokeStateHasChangedAsync(Func<Task> onAfterRender)
        {
            _onAfterRender = onAfterRender;
            return InvokeStateHasChangedAsync();
        }

        
    }
}
