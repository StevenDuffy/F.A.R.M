using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FarmControl
{
    public class Storage : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private byte storageNumber;
        private string storageType;
        private string cropStored;
        private string fertiliserStored;
        private short maxCapacity;
        private short usedCapacity;
        private byte storageTemperature;

        public byte StorageNumber
        {
            get { return this.storageNumber; }
            set
            {
                if (this.storageNumber != value)
                {
                    this.storageNumber = value;
                    this.NotifyPropertyChanged("StorageNumber");
                }
            }
        }

        public string StorageType
        {
            get { return this.storageType; }
            set
            {
                if (this.storageType != value)
                {
                    this.storageType = value;
                    this.NotifyPropertyChanged("StorageType");
                }
            }
        }

        public string CropStored
        {
            get { return this.cropStored; }
            set
            {
                if (this.cropStored != value)
                {
                    this.cropStored = value;
                    this.NotifyPropertyChanged("CropStored");
                }
            }
        }

        public string FertiliserStored
        {
            get { return this.fertiliserStored; }
            set
            {
                if (this.fertiliserStored != value)
                {
                    this.fertiliserStored = value;
                    this.NotifyPropertyChanged("FertiliserStored");
                }
            }
        }

        public short MaxCapacity
        {
            get { return this.maxCapacity; }
            set
            {
                if (this.maxCapacity != value)
                {
                    this.maxCapacity = value;
                    this.NotifyPropertyChanged("MaxCapacity");
                }
            }
        }

        public short UsedCapacity
        {
            get { return this.usedCapacity; }
            set
            {
                if (this.usedCapacity != value)
                {
                    this.usedCapacity = value;
                    this.NotifyPropertyChanged("UsedCapacity");
                }
            }
        }

        public byte StorageTemperature
        {
            get { return this.storageTemperature; }
            set
            {
                if (this.storageTemperature != value)
                {
                    this.storageTemperature = value;
                    this.NotifyPropertyChanged("StorageTemperature");
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
