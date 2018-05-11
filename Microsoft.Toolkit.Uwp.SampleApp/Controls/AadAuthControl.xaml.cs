// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using Microsoft.Toolkit.Uwp.UI.Controls.Graph;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Toolkit.Uwp.SampleApp.Controls
{
    public sealed partial class AadAuthControl : UserControl
    {
        internal static readonly DependencyProperty IsShowAadMetaDataControlProperty = DependencyProperty.Register(
            nameof(IsShowAadMetaDataControl),
            typeof(Visibility),
            typeof(AadAuthControl),
            new PropertyMetadata(Visibility.Visible));

        internal Visibility IsShowAadMetaDataControl
        {
            get { return (Visibility)GetValue(IsShowAadMetaDataControlProperty); }
            private set { SetValue(IsShowAadMetaDataControlProperty, value); }
        }

        internal static readonly DependencyProperty IsShowGraphControlProperty = DependencyProperty.Register(
            nameof(IsShowGraphControl),
            typeof(Visibility),
            typeof(AadAuthControl),
            new PropertyMetadata(Visibility.Collapsed));

        internal Visibility IsShowGraphControl
        {
            get { return (Visibility)GetValue(IsShowGraphControlProperty); }
            private set { SetValue(IsShowGraphControlProperty, value); }
        }

        private AadAuthenticationManager _aadAuthenticationManager = AadAuthenticationManager.Instance;

        static AadAuthControl()
        {
            AadAuthenticationManager.Instance.Initialize(
                "036118f3-9e5e-44e2-928d-d542a026d3c7",
                AadLogin.RequiredDelegatedPermissions,
                ProfileCard.RequiredDelegatedPermissions,
                PeoplePicker.RequiredDelegatedPermissions,
                SharePointFileList.RequiredDelegatedPermissions);
        }

        public AadAuthControl()
        {
            InitializeComponent();

            DataContext = _aadAuthenticationManager;

            _aadAuthenticationManager.PropertyChanged += AadAuthenticationManager_PropertyChanged;

            RereshControlVisibility();
        }

        private void AadAuthenticationManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_aadAuthenticationManager.IsAuthenticated))
            {
                RereshControlVisibility();
            }
        }

        private void RereshControlVisibility()
        {
            if (_aadAuthenticationManager.IsAuthenticated)
            {
                IsShowAadMetaDataControl = Visibility.Collapsed;
                IsShowGraphControl = Visibility.Visible;
            }
            else
            {
                IsShowAadMetaDataControl = Visibility.Visible;
                IsShowGraphControl = Visibility.Collapsed;
            }
        }
    }
}
