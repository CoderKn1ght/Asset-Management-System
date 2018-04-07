Asset Management System

The database has been seeded with test data.
The Credentials to be used for the application:
Admin User: admin
Admin Password: password

Resource Checker :
user: resource_checker
password: password

Additional functionalies implemented:

1)A mail has been sent to user regarding the application url, responsibilities and login credentials. 
2)Implemented Password Hashing for improving Application Security.
3)Authorization based on role to avoid unauthorized access.
4)Super admin cannot be deleted.
5)Users with same username cannot be created.



Implementation of the application is based on the requirement document supplied.

Use Case 1 : Inventory check:
The resource checker checks the inventory of the resources based on the facilities assigned to him.
He updates the values and fills in a mandatory comment.
 
Use Case 2 : Adding the user:
The admin can create users and set facilities to them. For an admin, all the facilities are assigned.

Use Case 3 : Edit the user:
An admin can edit the details of an user.

Delete/Disable User:
An Admin can delete the user which would assign him as inactive.

View Report:
The admin is able to see a report for all the facilities. In case of a change in the value of a resource, the facility is seen in red. When he validates the changes, it turns to green.

Add Facilities: User with admin role is able to add the facilities and its details.
 
Adding Resources:Admin user is able to add resources to the facilities from the resources tab.

Edit Facilities/Resources:User is able to edit the details for the resources or the facilities.

Delete Resources:User with admin role can delete the resources. 

Delete Facility: User with admin role is able to delete the facilities.




