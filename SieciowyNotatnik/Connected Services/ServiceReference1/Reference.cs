﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace z7WinForms.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IwcfService")]
    public interface IwcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/DBis", ReplyAction="http://tempuri.org/IwcfService/DBisResponse")]
        bool DBis();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/DBis", ReplyAction="http://tempuri.org/IwcfService/DBisResponse")]
        System.Threading.Tasks.Task<bool> DBisAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/addNewUser", ReplyAction="http://tempuri.org/IwcfService/addNewUserResponse")]
        bool addNewUser(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/addNewUser", ReplyAction="http://tempuri.org/IwcfService/addNewUserResponse")]
        System.Threading.Tasks.Task<bool> addNewUserAsync(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/getUserId", ReplyAction="http://tempuri.org/IwcfService/getUserIdResponse")]
        int getUserId(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/getUserId", ReplyAction="http://tempuri.org/IwcfService/getUserIdResponse")]
        System.Threading.Tasks.Task<int> getUserIdAsync(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/addNewText", ReplyAction="http://tempuri.org/IwcfService/addNewTextResponse")]
        int addNewText(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/addNewText", ReplyAction="http://tempuri.org/IwcfService/addNewTextResponse")]
        System.Threading.Tasks.Task<int> addNewTextAsync(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/getTexts", ReplyAction="http://tempuri.org/IwcfService/getTextsResponse")]
        System.Collections.Generic.Dictionary<int, string> getTexts(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/getTexts", ReplyAction="http://tempuri.org/IwcfService/getTextsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<int, string>> getTextsAsync(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/updateText", ReplyAction="http://tempuri.org/IwcfService/updateTextResponse")]
        bool updateText(int textID, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/updateText", ReplyAction="http://tempuri.org/IwcfService/updateTextResponse")]
        System.Threading.Tasks.Task<bool> updateTextAsync(int textID, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/userExist", ReplyAction="http://tempuri.org/IwcfService/userExistResponse")]
        bool userExist(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/userExist", ReplyAction="http://tempuri.org/IwcfService/userExistResponse")]
        System.Threading.Tasks.Task<bool> userExistAsync(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/firstText", ReplyAction="http://tempuri.org/IwcfService/firstTextResponse")]
        string firstText(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/firstText", ReplyAction="http://tempuri.org/IwcfService/firstTextResponse")]
        System.Threading.Tasks.Task<string> firstTextAsync(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/firstTextID", ReplyAction="http://tempuri.org/IwcfService/firstTextIDResponse")]
        int firstTextID(string user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwcfService/firstTextID", ReplyAction="http://tempuri.org/IwcfService/firstTextIDResponse")]
        System.Threading.Tasks.Task<int> firstTextIDAsync(string user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IwcfServiceChannel : z7WinForms.ServiceReference1.IwcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IwcfServiceClient : System.ServiceModel.ClientBase<z7WinForms.ServiceReference1.IwcfService>, z7WinForms.ServiceReference1.IwcfService {
        
        public IwcfServiceClient() {
        }
        
        public IwcfServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IwcfServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IwcfServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IwcfServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool DBis() {
            return base.Channel.DBis();
        }
        
        public System.Threading.Tasks.Task<bool> DBisAsync() {
            return base.Channel.DBisAsync();
        }
        
        public bool addNewUser(string user) {
            return base.Channel.addNewUser(user);
        }
        
        public System.Threading.Tasks.Task<bool> addNewUserAsync(string user) {
            return base.Channel.addNewUserAsync(user);
        }
        
        public int getUserId(string user) {
            return base.Channel.getUserId(user);
        }
        
        public System.Threading.Tasks.Task<int> getUserIdAsync(string user) {
            return base.Channel.getUserIdAsync(user);
        }
        
        public int addNewText(string user) {
            return base.Channel.addNewText(user);
        }
        
        public System.Threading.Tasks.Task<int> addNewTextAsync(string user) {
            return base.Channel.addNewTextAsync(user);
        }
        
        public System.Collections.Generic.Dictionary<int, string> getTexts(string user) {
            return base.Channel.getTexts(user);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<int, string>> getTextsAsync(string user) {
            return base.Channel.getTextsAsync(user);
        }
        
        public bool updateText(int textID, string text) {
            return base.Channel.updateText(textID, text);
        }
        
        public System.Threading.Tasks.Task<bool> updateTextAsync(int textID, string text) {
            return base.Channel.updateTextAsync(textID, text);
        }
        
        public bool userExist(string user) {
            return base.Channel.userExist(user);
        }
        
        public System.Threading.Tasks.Task<bool> userExistAsync(string user) {
            return base.Channel.userExistAsync(user);
        }
        
        public string firstText(string user) {
            return base.Channel.firstText(user);
        }
        
        public System.Threading.Tasks.Task<string> firstTextAsync(string user) {
            return base.Channel.firstTextAsync(user);
        }
        
        public int firstTextID(string user) {
            return base.Channel.firstTextID(user);
        }
        
        public System.Threading.Tasks.Task<int> firstTextIDAsync(string user) {
            return base.Channel.firstTextIDAsync(user);
        }
    }
}
