# My-Clinic-Api
- My clinic is the easiest way of booking the best clinics. using .NET core web API.
# About
- Help visitors to view doctors with different department and area. Make rate, review and book etc..
- Doctors can register thim data to join website, and show all vistors rate, reveiwes and booking.
- Admin manage all operations insid websit
# Tools
- Repository design pattern
- Automapper
- JWT
- Identity
- EF core

# Clone Repo
```
$ git clone https://github.com/hameedhassandev/My-Clinic-API
```
# Currant Endpoints
- Department
```http
GET /api/Department/GetDepartmentById/{id}
GET /api/DepartmentGetDepartmentByIdWithData/{id}
GET /api/Department/GetAll
GET /api/Department/GetAllWithData
GET /api/Department/GetAllPagination
GET /api/Department/GetAllWithFilteration
GET /api/Department/GetAllWithFilterationAndPagination
POST /api/Department/AddDepartment
PUT /api/Department/UpdateDepartment/{id}
DELETE /api/Department/DeleteDepartment/{id}
```

- Specialist
```http
GET /api/Specialist/GetAll
GET /api/Specialist/GetAllWithData
GET /api/Specialist/GetById/{id}
GET /api/Specialist/GetByIdWithData/{id}
POST /api/Specialist/AddSpecialist
POST /api/Specialist/AddSpecialistToDoctor
DELETE /api/Specialist/DeleteSpecialist/{id}
```


- Hospital
```http
GET /api/Hospital/GetById/{id}
GET /api/Hospital//GetByIdWithData/{id}
GET /api/Hospital/GetAll
GET /api/Hospital/GetAllWithData
GET /api/Hospital/GetAllPagination
GET /api/Hospital/GetAllWithFilteration
GET /api/Hospital/GetAllWithFilterationAnfPagination
POST /api/Hospital/AddHospital
PUT /api/Hospital/UpdateHospital/{id}
DELETE /api/Hospital/DeleteHospital/{id}
```
- Insurance
```http
GET /api/Insurance/GetById/{id}
GET /api/Insurance//GetByIdWithData/{id}
GET /api/Insurance/GetAll
GET /api/Insurance/GetAllWithData
GET /api/Insurance/GetAllPagination
GET /api/Insurance/GetAllWithFilteration
GET /api/Insurance/GetAllWithFilterationAnfPagination
POST /api/Insurance/AddInsurance
PUT /api/Insurance/UpdateInsurance/{id}
DELETE /api/Insurance/DeleteInsurance/{id}
```
- TimesOfWork
```http
GET /api/TimesOfWork/GetAll
GET /api/TimesOfWork/GetAllWithData
GET /api/TimesOfWork/GetById/{id}
GET /api/TimesOfWork/GetByIdWithData/{id}
GET /api/TimesOfWork/GetDatesOfDoctor/{doctorId}
GET /api/TimesOfWork/GetAllTimesOfDoctor/{doctorId}
GET /api/TimesOfWork/GetAvailableTimesOfDctorNest3Days/{doctorId}
POST /api/TimesOfWork/AddTimeToDoctor/{doctorId}
PUT /api/TimesOfWork/UpdateTimeOfDoctor/{doctorId}
DELETE /api/TimesOfWork/DeleteTimeOfDoctor/{doctorId}
```
- Area
```http
GET /api/Area/GetAreaById/{id}
GET /api/Area/GetAllAreas
POST /api/Area/AddArea
PUT /api/Area/UpdateArea
DELETE /api/Area/DeleteArea/{id}
```


- Authorization
```http
POST /api/Auth/RegisterationAsPatient
POST /api/Auth/RegisterationAsDoctor
POST /api/Auth/AddDoctorByAdmin
POST /api/Auth/Login
POST /api/Auth/AddRole
PUT /api/Auth/ConfirmDoctorByAdmin/{doctorId}
GET /api/Auth/AllRoles
GET /api/Auth/ConfirmEmail
```
- Book
```http
GET /api/Book/GetAll
GET /api/Book/GetAllWithData
GET /api/Book/GetBookingsWithDoctor
POST /api/Book/AddBook
```
- RateAndReview
```http
GET /api/RateAndReview/GetAll
GET /api/RateAndReview/GetAllWithData
GET /api/RateAndReview/GetById/{id}
GET /api/RateAndReview/GetByIdWithData/{id}
GET /api/RateAndReview/GetReviewsOfDoctor
GET /api/RateAndReview/GetReviewsOfPatient
POST /api/RateAndReview/AddRateAndReview
DELETE /api/RateAndReview/DeleteRateAndReview
```
- Patient
```http
GET /api/Patient/GetAll
GET /api/Patient/GetAllWithData
GET /api/Patient/GetPatientById/{id}
GET /api/Patient/GetPatientByIdWithData/{id}
```

- Reasons
```http
GET /api/Reasons/GetAll
GET /api/Reasons/GetAllWithData
GET /api/Reasons/GetById/{id}
GET /api/Reasons/GetByIdWithData/{id}
POST /api/Reasons/AddReason
DELETE /api/Reasons/DeleteReason/{id}
```

- Report
```http
GET /api/Report/GetAll
GET /api/Report/GetAllWithData
GET /api/Report/GetById/{id}
GET /api/Report/GetByIdWithData/{id}
POST /api/Report/AddReport
```

- Enums
```http
GET /api/Enums/GetValuesOfCities
GET /api/Enums/GetValuesOfGender
GET /api/Enums/GetValuesOfDays
```

# contributors
- Amr Rizk
- Hameed Hassan


