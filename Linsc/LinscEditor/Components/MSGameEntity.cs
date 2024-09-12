using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinscEditor.Components
{
    internal abstract class MSEntity : ViewModelBase
    {
        private bool _updatesEnabled = true;

        private bool? _isEnabled;
        public bool? IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private readonly ObservableCollection<IMSComponent> _components = new();
        public ReadOnlyObservableCollection<IMSComponent> Components;

        public List<GameEntity> SelectedEntities { get; }
        

        protected MSEntity(List<GameEntity> selectedEntities)
        {
            Debug.Assert(selectedEntities?.Any() == true);
            Components = new(_components);
            SelectedEntities = selectedEntities;
            PropertyChanged += (s, e) => { if (_updatesEnabled) UpdateGameEntities(e.PropertyName); };
        }

        protected virtual bool UpdateGameEntities(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IsEnabled): SelectedEntities.ForEach(x => x.IsEnabled = IsEnabled.Value); return true;
                case nameof(Name): SelectedEntities.ForEach(x => x.Name = Name); return true;
            }
            return false;
        }

        public void Refresh()
        {
            _updatesEnabled = false;
            UpdateMSGameEntity();
            _updatesEnabled = true;
        }

        protected virtual bool UpdateMSGameEntity()
        {
            IsEnabled = GetMixedValue(SelectedEntities, new Func<GameEntity, bool>(x => x.IsEnabled));   
            Name = GetMixedValue(SelectedEntities, new Func<GameEntity, string>(x => x.Name));

            return true;
        }

        #region GetMixedValues
        private static bool? GetMixedValue(List<GameEntity> selectedEntities, Func<GameEntity, bool> getProperty)
        {
            bool? value = getProperty(selectedEntities.First());
            foreach(var entity in selectedEntities.Skip(1))
            {
                if(value != getProperty(entity))
                {
                    return null;
                }
            }
            return value;
        }

        private static string GetMixedValue(List<GameEntity> selectedEntities, Func<GameEntity, string> getProperty)
        {
            string value = getProperty(selectedEntities.First());
            foreach (var entity in selectedEntities.Skip(1))
            {
                if (value != getProperty(entity))
                {
                    return null;
                }
            }
            return value;
        }

        private static float? GetMixedValue(List<GameEntity> selectedEntities, Func<GameEntity, float> getProperty)
        {
            float value = getProperty(selectedEntities.First());
            foreach (var entity in selectedEntities.Skip(1))
            {
                if (value != getProperty(entity))
                {
                    return null;
                }
            }
            return value;
        }
        #endregion
    }

    internal class MSGameEntity : MSEntity
    {

    }
}
