
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LinscEditor.Utilities
{
    public interface IUndoRedo
    {
        public string Name { get; }

        public void Undo();
        public void Redo();
    }

    public class UndoRedoAction : IUndoRedo
    {
        private Action _undoAction;
        private Action _redoAction;

        public string Name { get; }

        public void Undo() => _undoAction();
        public void Redo() => _redoAction();


        public UndoRedoAction(string name)
        {
            Name = name;
        }

        public UndoRedoAction(Action undoAction, Action redoAction, string name)
            : this(name)
        {
            Debug.Assert(undoAction != null &&  redoAction != null);
            _undoAction = undoAction;
            _redoAction = redoAction;
        }
    }

    public class UndoRedo
    {
        private ObservableCollection<IUndoRedo> _undoList = new();
        public ReadOnlyObservableCollection<IUndoRedo> UndoList { get; }

        private ObservableCollection<IUndoRedo> _redoList = new();
        public ReadOnlyObservableCollection<IUndoRedo> RedoList {  get; }

        public UndoRedo()
        {
            UndoList = new(_undoList);
            RedoList = new(_redoList);
        }

        public void Add(IUndoRedo command)
        {
            _undoList.Add(command);
            _redoList.Clear();
        }

        public void Reset()
        {
            _undoList.Clear();
            _redoList.Clear();
        }

        public void Undo()
        {
            if (_undoList.Any())
            {
                var command = _undoList.Last();
                command.Undo();
                _redoList.Add(command);
                _undoList.RemoveAt(_undoList.Count - 1);
            }
        }

        public void Redo()
        {
            if (_redoList.Any())
            {
                var command = _redoList.Last();
                command.Redo();
                _undoList.Add(command);
                _redoList.RemoveAt(_redoList.Count-1);
            }
        }
    }
}
