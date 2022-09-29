using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Misc
{
    public enum LogItemType
    {
        AccountActivated = 0, AccountDeActivated = 1, StoreSettingsUpdated = 2, ChangedPassword = 3,
        AccountCreated = 4, CreateNewAdmin = 5, ActivateAccount = 6, DeActivateAccount = 7,
        CreateNewRole = 8, AddedUserToRole = 9, UserAddedToRole = 10, DeletedRole = 11,
        ChangedRoleName = 12, ActivatedCategory = 13, DisActivatedCategory = 14, CreatedCategory = 15,
        UpdatedCategory = 16, NewProduct = 17, UpdateProduct = 18, StartProduct = 19, StopProduct = 20
    }

    public enum ProductState { Pending = 0, Approved = 1, Suspended = 2, Stopped = 3 }
    //product placed by vendor then approved or suspended by operator
    //may be stopped by vendor for a while
    //when user add/edit product then the state will be pending
    
  
}
