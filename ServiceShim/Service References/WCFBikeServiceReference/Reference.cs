﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BdcModelBuilder.ServiceShim.WCFBikeServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MountainBike", Namespace="http://schemas.datacontract.org/2004/07/WebberCross.BdcModelBuilder.WcfBikeServic" +
        "e")]
    [System.SerializableAttribute()]
    public partial class MountainBike : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BrakeSetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ForksField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ManufacturerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QuantityInStockField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RearShockField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BrakeSet {
            get {
                return this.BrakeSetField;
            }
            set {
                if ((object.ReferenceEquals(this.BrakeSetField, value) != true)) {
                    this.BrakeSetField = value;
                    this.RaisePropertyChanged("BrakeSet");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Forks {
            get {
                return this.ForksField;
            }
            set {
                if ((object.ReferenceEquals(this.ForksField, value) != true)) {
                    this.ForksField = value;
                    this.RaisePropertyChanged("Forks");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Manufacturer {
            get {
                return this.ManufacturerField;
            }
            set {
                if ((object.ReferenceEquals(this.ManufacturerField, value) != true)) {
                    this.ManufacturerField = value;
                    this.RaisePropertyChanged("Manufacturer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Model {
            get {
                return this.ModelField;
            }
            set {
                if ((object.ReferenceEquals(this.ModelField, value) != true)) {
                    this.ModelField = value;
                    this.RaisePropertyChanged("Model");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int QuantityInStock {
            get {
                return this.QuantityInStockField;
            }
            set {
                if ((this.QuantityInStockField.Equals(value) != true)) {
                    this.QuantityInStockField = value;
                    this.RaisePropertyChanged("QuantityInStock");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RearShock {
            get {
                return this.RearShockField;
            }
            set {
                if ((object.ReferenceEquals(this.RearShockField, value) != true)) {
                    this.RearShockField = value;
                    this.RaisePropertyChanged("RearShock");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BusinessDataEntity", Namespace="http://schemas.datacontract.org/2004/07/WebberCross.BdcModelBuilder.WcfBikeServic" +
        "e")]
    [System.SerializableAttribute()]
    public partial class BusinessDataEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike BikeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike[] BikesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ManufacturerIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModelIDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike Bike {
            get {
                return this.BikeField;
            }
            set {
                if ((object.ReferenceEquals(this.BikeField, value) != true)) {
                    this.BikeField = value;
                    this.RaisePropertyChanged("Bike");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike[] Bikes {
            get {
                return this.BikesField;
            }
            set {
                if ((object.ReferenceEquals(this.BikesField, value) != true)) {
                    this.BikesField = value;
                    this.RaisePropertyChanged("Bikes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ManufacturerID {
            get {
                return this.ManufacturerIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ManufacturerIDField, value) != true)) {
                    this.ManufacturerIDField = value;
                    this.RaisePropertyChanged("ManufacturerID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModelID {
            get {
                return this.ModelIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ModelIDField, value) != true)) {
                    this.ModelIDField = value;
                    this.RaisePropertyChanged("ModelID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFBikeServiceReference.IBikeService")]
    public interface IBikeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/GetMountainBikes", ReplyAction="http://tempuri.org/IBikeService/GetMountainBikesResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike[] GetMountainBikes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/GetMountainBike", ReplyAction="http://tempuri.org/IBikeService/GetMountainBikeResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike GetMountainBike(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/AddMountainBike", ReplyAction="http://tempuri.org/IBikeService/AddMountainBikeResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike AddMountainBike(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike bike);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/DeleteMountainBike", ReplyAction="http://tempuri.org/IBikeService/DeleteMountainBikeResponse")]
        void DeleteMountainBike(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/GetMountainBikesComplex", ReplyAction="http://tempuri.org/IBikeService/GetMountainBikesComplexResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity GetMountainBikesComplex();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/GetMountainBikeComplex", ReplyAction="http://tempuri.org/IBikeService/GetMountainBikeComplexResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity GetMountainBikeComplex(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/GetMountainBikeComplexComposite", ReplyAction="http://tempuri.org/IBikeService/GetMountainBikeComplexCompositeResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity GetMountainBikeComplexComposite(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/AddMountainBikeComplex", ReplyAction="http://tempuri.org/IBikeService/AddMountainBikeComplexResponse")]
        BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity AddMountainBikeComplex(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/DeleteMountainBikeComplex", ReplyAction="http://tempuri.org/IBikeService/DeleteMountainBikeComplexResponse")]
        void DeleteMountainBikeComplex(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeService/DeleteMountainBikeComplexComposite", ReplyAction="http://tempuri.org/IBikeService/DeleteMountainBikeComplexCompositeResponse")]
        void DeleteMountainBikeComplexComposite(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBikeServiceChannel : BdcModelBuilder.ServiceShim.WCFBikeServiceReference.IBikeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BikeServiceClient : System.ServiceModel.ClientBase<BdcModelBuilder.ServiceShim.WCFBikeServiceReference.IBikeService>, BdcModelBuilder.ServiceShim.WCFBikeServiceReference.IBikeService {
        
        public BikeServiceClient() {
        }
        
        public BikeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BikeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BikeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BikeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike[] GetMountainBikes() {
            return base.Channel.GetMountainBikes();
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike GetMountainBike(int id) {
            return base.Channel.GetMountainBike(id);
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike AddMountainBike(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.MountainBike bike) {
            return base.Channel.AddMountainBike(bike);
        }
        
        public void DeleteMountainBike(int id) {
            base.Channel.DeleteMountainBike(id);
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity GetMountainBikesComplex() {
            return base.Channel.GetMountainBikesComplex();
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity GetMountainBikeComplex(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde) {
            return base.Channel.GetMountainBikeComplex(bde);
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity GetMountainBikeComplexComposite(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde) {
            return base.Channel.GetMountainBikeComplexComposite(bde);
        }
        
        public BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity AddMountainBikeComplex(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde) {
            return base.Channel.AddMountainBikeComplex(bde);
        }
        
        public void DeleteMountainBikeComplex(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde) {
            base.Channel.DeleteMountainBikeComplex(bde);
        }
        
        public void DeleteMountainBikeComplexComposite(BdcModelBuilder.ServiceShim.WCFBikeServiceReference.BusinessDataEntity bde) {
            base.Channel.DeleteMountainBikeComplexComposite(bde);
        }
    }
}