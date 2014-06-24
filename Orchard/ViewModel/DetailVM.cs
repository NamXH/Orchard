using System;
using Xamarin.Forms;
using System.IO;
using PCLStorage;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Orchard
{
    public class DetailVM<T> : NPCBase where T : IDataItem, new()
    {
        public DetailVM(Func<T, T> copyFunc, Action<T, T> updateAct)
        {
            _copyFunc = copyFunc;
            _updateAct = updateAct;
        }

        Func<T, T> _copyFunc;
        Action<T, T> _updateAct;

        T _currItem;

        public T CurrItem
        { 
            get { return _currItem; }
            set
            {
                _currItem = value;
                if (_currItem == null)
                {
                    LocalItem = default(T);
                }
                else
                {
                    LocalItem = _copyFunc.Invoke(_currItem);
                }
            }
        }

        public T LocalItem { get; private set; }

        public bool IsEditing
        {
            get
            {
                return CurrItem != null;
            }
        }

        public async Task ChangeImg(Stream photoStream)
        {
            using (photoStream)
            {
                Debug.WriteLine("Length: {0}", photoStream.Length);
                var checkFolder = await FileSystem.Current.LocalStorage.CheckExistsAsync(Helper.PictureFolderForType<Operator>());
                if (checkFolder == ExistenceCheckResult.NotFound)
                {
                    await FileSystem.Current.LocalStorage.CreateFolderAsync(Helper.PictureFolderForType<Operator>(), CreationCollisionOption.FailIfExists);
                }
                if (LocalItem.Id == 0)
                {
                    // TODO: get next id from db and assign it here.

                }
                var localFile = await FileSystem.Current.LocalStorage.CreateFileAsync(
                                    LocalItem.Image,
                                    CreationCollisionOption.ReplaceExisting);
                using (var lfStream = await localFile.OpenAsync(FileAccess.ReadAndWrite))
                {
                    await photoStream.CopyToAsync(lfStream);
                }
            }
        }

        Command _doneCmd;

        public Command DoneCmd
        {
            get
            {
                if (_doneCmd == null)
                {

                    _doneCmd = new Command(arg =>
                    {
                        if (IsEditing)
                        {
                            DbManager.Update(LocalItem);
                            // Update all needed properties.
                            _updateAct(CurrItem, LocalItem);
                        }
                        else
                        {
                            // Adding a new item.
                            DbManager.AddItem(LocalItem);
                        }
                    });
                }
                return _doneCmd;
            }
        }

        Command _delCmd;

        public Command DelCmd
        {
            get
            {
                if (_delCmd == null)
                {
                    _delCmd = new Command(arg =>
                    {
                        DbManager.DeleteItem(CurrItem);
                    });
                }
                return _delCmd;
            }
        }
    }
}

