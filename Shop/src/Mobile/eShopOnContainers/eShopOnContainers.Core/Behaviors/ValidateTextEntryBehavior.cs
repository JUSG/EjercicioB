using System;
using Xamarin.Forms;

namespace eShopOnContainers.Core.Behaviors
{
    /// <summary>
    /// Text entry behavior
    /// </summary>
    public class ValidateTextEntryBehavior : Behavior<Entry>
    {
        #region Properties

        /// <summary>
        /// attached view
        /// </summary>
        private Entry attachedView;

        /// <summary>
        /// validating the text
        /// </summary>
        private bool validatingText;

        /// <summary>
        /// Text is valid
        /// </summary>
        public bool TextValid
        {
            get { return (bool)GetValue(TextValidProperty); }
            set { SetValue(TextValidProperty, value); }
        }

        /// <summary>
        /// Max text length bindable property
        /// </summary>
        public static readonly BindableProperty TextValidProperty = BindableProperty.Create(nameof(TextValid),
                                                                                            typeof(bool),
                                                                                            typeof(ValidateTextEntryBehavior),
                                                                                            default(bool),
                                                                                            BindingMode.TwoWay);

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets event
        /// </summary>
        /// <param name="bindable">Text Entry</param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            attachedView = bindable;
            attachedView.BindingContextChanged += OnAttachedViewBindingContextChanged;

            bindable.TextChanged += HandleTextChanged;
        }

        /// <summary>
        /// Unsuscribes event
        /// </summary>
        /// <param name="bindable">Text Entry</param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }

        /// <summary>
        /// Validates Name text changed
        /// </summary>
        /// <param name="sender">Object to validate the change</param>
        /// <param name="e">>Text Changed Events Args</param>
        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!validatingText)
            {
                validatingText = true;

                TextValid = !string.IsNullOrWhiteSpace(e.NewTextValue);               

                validatingText = false;
            }
        }

        /// <summary>
        /// On attached view binding context text changed
        /// </summary>
        /// <param name="sender">object calling method</param>
        /// <param name="e">Event arguments</param>
        private void OnAttachedViewBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = attachedView.BindingContext;
        }

        #endregion Methods

    }
}
