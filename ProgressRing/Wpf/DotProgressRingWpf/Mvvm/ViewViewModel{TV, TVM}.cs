using System;
using System.ComponentModel;

namespace Mvvm
{
    /// <summary>
    /// The View and View-Model set. This only holds a view and a view-model instance.
    /// </summary>
    /// <typeparam name="TV">The type of a view.</typeparam>
    /// <typeparam name="TVM">The type of a view-model.</typeparam>
    public class ViewViewModel<TV, TVM>
        where TV : class
        where TVM : class, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewViewModel{TV, TVM}" /> class.
        /// </summary>
        /// <param name="view">The view instance.</param>
        /// <param name="viewModel">The view-model instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="view"/> or <paramref name="viewModel"/> is <c>null</c>.</exception>
        public ViewViewModel(TV view, TVM viewModel)
        {
            if (view is null)
                throw new ArgumentNullException(nameof(view));

            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            this.View = view;
            this.ViewModel = viewModel;
        }

        /// <summary>
        /// Gets a view instance.
        /// </summary>
        public TV View { get; }

        /// <summary>
        /// Gets a view-model instance.
        /// </summary>
        public TVM ViewModel { get; }
    }
}
