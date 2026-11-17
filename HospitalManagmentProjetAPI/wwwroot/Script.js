"use strict";

console.log("Hospital frontend JavaScript started");


// ==========================================
// BACKEND API ROUTES
// ==========================================

const patientApi = "/api/patient";
const doctorApi = "/api/doctor";
const appointmentApi = "/api/Appointment";
// ==========================================
// HELPER: GET HTML ELEMENT
// ==========================================

function getElement(id) {
    return document.getElementById(id);
}


// ==========================================
// HELPER: READ DIFFERENT PROPERTY NAMES
// ==========================================

function readValue(object, ...names) {

    for (const name of names) {

        if (
            object &&
            object[name] !== undefined &&
            object[name] !== null
        ) {
            return object[name];
        }
    }

    return "";
}


// ==========================================
// HELPER: DISPLAY DATE
// ==========================================

function displayDate(dateValue) {

    if (!dateValue) {
        return "";
    }

    const date = new Date(dateValue);

    if (isNaN(date.getTime())) {
        return dateValue;
    }

    return date.toLocaleDateString("en-IN");
}


// ==========================================
// SIDEBAR NAVIGATION
// ==========================================

document
    .querySelectorAll(".nav-link")
    .forEach(function (button) {

        button.addEventListener(
            "click",
            function () {

                // Remove active style from all buttons

                document
                    .querySelectorAll(".nav-link")
                    .forEach(function (item) {

                        item.classList.remove("active");

                    });


                // Hide all page sections

                document
                    .querySelectorAll(".page-section")
                    .forEach(function (section) {

                        section.classList.remove(
                            "active-section"
                        );

                    });


                // Activate clicked button

                button.classList.add("active");


                // Show selected section

                const sectionId =
                    button.dataset.section;

                const selectedSection =
                    getElement(sectionId);

                if (selectedSection) {

                    selectedSection.classList.add(
                        "active-section"
                    );

                }


                // Change subtitle

                const subtitle =
                    getElement("pageSubtitle");

                if (subtitle) {

                    subtitle.innerText =
                        button.innerText.trim();

                }


                // Reload selected data

                if (sectionId === "patientsSection") {
                    loadPatients();
                }

                if (sectionId === "doctorsSection") {
                    loadDoctors();
                }

                if (sectionId === "appointmentsSection") {
                    loadAppointments();
                }

            }
        );

    });


// ==========================================
// 1. LOAD PATIENTS
// GET /api/patient
// ==========================================

async function loadPatients() {

    try {

        const response =
            await fetch(patientApi);


        if (!response.ok) {

            throw new Error(
                "Patient API returned status " +
                response.status
            );

        }


        const patients =
            await response.json();


        console.log(
            "Patients received:",
            patients
        );


        // Update dashboard patient count

        const patientCount =
            getElement("patientCount");

        if (patientCount) {

            patientCount.innerText =
                patients.length;

        }


        let patientRows = "";
        let dashboardRows = "";


        patients.forEach(function (patient) {

            const id = readValue(
                patient,
                "pid",
                "Pid"
            );


            const name = readValue(
                patient,
                "pName",
                "pname",
                "PName"
            );


            const birthDate = readValue(
                patient,
                "birthDate",
                "birthdate",
                "BirthDate"
            );


            const phoneNumber = readValue(
                patient,
                "phoneNo",
                "phoneno",
                "PhoneNo"
            );


            // Patient Management table

            patientRows += `

                <tr>

                    <td>${id}</td>

                    <td>${name}</td>

                    <td>
                        ${displayDate(birthDate)}
                    </td>

                    <td>${phoneNumber}</td>

                    <td>

                        <button
                            class="edit-button"
                            onclick="editPatient(${id})">

                            Edit

                        </button>

                        <button
                            class="delete-button"
                            onclick="deletePatient(${id})">

                            Delete

                        </button>

                    </td>

                </tr>

            `;

        });


        // Display first five patients on dashboard

        patients
            .slice(0, 5)
            .forEach(function (patient) {

                const id = readValue(
                    patient,
                    "pid",
                    "Pid"
                );


                const name = readValue(
                    patient,
                    "pName",
                    "pname",
                    "PName"
                );


                const birthDate = readValue(
                    patient,
                    "birthDate",
                    "birthdate",
                    "BirthDate"
                );


                const phoneNumber = readValue(
                    patient,
                    "phoneNo",
                    "phoneno",
                    "PhoneNo"
                );


                dashboardRows += `

                    <tr>

                        <td>${id}</td>

                        <td>${name}</td>

                        <td>
                            ${displayDate(birthDate)}
                        </td>

                        <td>${phoneNumber}</td>

                    </tr>

                `;

            });


        const patientTable =
            getElement("patientTable");

        if (patientTable) {

            patientTable.innerHTML =
                patientRows ||
                `
                    <tr>
                        <td colspan="5">
                            No patients found
                        </td>
                    </tr>
                `;

        }


        const dashboardTable =
            getElement("dashboardPatientTable");

        if (dashboardTable) {

            dashboardTable.innerHTML =
                dashboardRows ||
                `
                    <tr>
                        <td colspan="4">
                            No patients found
                        </td>
                    </tr>
                `;

        }

    }
    catch (error) {

        console.error(
            "Patient connection error:",
            error
        );


        const dashboardTable =
            getElement("dashboardPatientTable");

        if (dashboardTable) {

            dashboardTable.innerHTML = `

                <tr>

                    <td colspan="4">

                        Patient API connection failed:
                        ${error.message}

                    </td>

                </tr>

            `;

        }

    }

}


// ==========================================
// 2. LOAD DOCTORS
// GET /api/doctor
// ==========================================

async function loadDoctors() {

    try {

        const response =
            await fetch(doctorApi);


        if (!response.ok) {

            throw new Error(
                "Doctor API returned status " +
                response.status
            );

        }


        const doctors =
            await response.json();


        console.log(
            "Doctors received:",
            doctors
        );


        const doctorCount =
            getElement("doctorCount");

        if (doctorCount) {

            doctorCount.innerText =
                doctors.length;

        }


        const departments =
            new Set();


        let doctorRows = "";


        doctors.forEach(function (doctor) {

            const doctorId = readValue(
                doctor,
                "drId",
                "dr_id",
                "DrId"
            );


            const departmentId = readValue(
                doctor,
                "deptId",
                "dept_id",
                "DeptId"
            );


            const doctorName = readValue(
                doctor,
                "name",
                "Name"
            );


            const speciality = readValue(
                doctor,
                "specialty",
                "Specialty"
            );


            departments.add(departmentId);


            doctorRows += `

                <tr>

                    <td>${doctorId}</td>

                    <td>${departmentId}</td>

                    <td>${doctorName}</td>

                    <td>${speciality}</td>

                    <td>

                        <button
                            class="edit-button"
                            onclick="editDoctor(${doctorId})">

                            Edit

                        </button>

                        <button
                            class="delete-button"
                            onclick="deleteDoctor(${doctorId})">

                            Delete

                        </button>

                    </td>

                </tr>

            `;

        });


        const departmentCount =
            getElement("departmentCount");

        if (departmentCount) {

            departmentCount.innerText =
                departments.size;

        }


        const doctorTable =
            getElement("doctorTable");

        if (doctorTable) {

            doctorTable.innerHTML =
                doctorRows ||
                `
                    <tr>
                        <td colspan="5">
                            No doctors found
                        </td>
                    </tr>
                `;

        }

    }
    catch (error) {

        console.error(
            "Doctor connection error:",
            error
        );


        const doctorTable =
            getElement("doctorTable");

        if (doctorTable) {

            doctorTable.innerHTML = `

                <tr>

                    <td colspan="5">

                        Doctor API connection failed:
                        ${error.message}

                    </td>

                </tr>

            `;

        }

    }

}


// ==========================================
// 3. LOAD APPOINTMENTS
// GET /api/Appointment
// ==========================================

async function loadAppointments() {

    const table =
        document.getElementById("appointmentTable");

    try {

        const response = await fetch(
            "https://localhost:7068/api/Appointment",
            {
                method: "GET",
                cache: "no-store"
            }
        );

        if (!response.ok) {

            throw new Error(
                "Status: " + response.status
            );
        }

        const appointments =
            await response.json();

        console.log(
            "Appointment data:",
            appointments
        );

        // Dashboard count

        const count =
            document.getElementById(
                "appointmentCount"
            );

        if (count) {
            count.innerText =
                appointments.length;
        }


        // Clear old error/message

        table.innerHTML = "";


        // Show database records

        appointments.forEach(
            function (appointment) {

                const date =
                    new Date(
                        appointment.schedule
                    ).toLocaleDateString("en-IN");


                table.innerHTML += `

                    <tr>

                        <td>
                            ${appointment.aid}
                        </td>

                        <td>
                            ${appointment.pid}
                        </td>

                        <td>
                            ${appointment.drId}
                        </td>

                        <td>
                            ${date}
                        </td>

                        <td>
                            ${appointment.status}
                        </td>

                        <td>
                            <button
                                class="edit-button"
                                onclick="editAppointment(${appointment.aid})">

                                Edit

                            </button>

                            <button
                                class="delete-button"
                                onclick="deleteAppointment(${appointment.aid})">

                                Delete

                            </button>
                        </td>

                    </tr>
                `;
            }
        );


        if (appointments.length === 0) {

            table.innerHTML = `

                <tr>
                    <td colspan="6">
                        No appointments found
                    </td>
                </tr>

            `;
        }

    }
    catch (error) {

        console.error(error);

        table.innerHTML = `

            <tr>
                <td colspan="6">
                    Appointment failed:
                    ${error.message}
                </td>
            </tr>

        `;
    }
}
// ==========================================
// TEMPORARY BUTTON FUNCTIONS
// These prevent JavaScript errors until CRUD
// is connected in the next step.
// ==========================================

function editPatient(id) {

    alert(
        "Patient Edit selected: " + id
    );

}


function deletePatient(id) {

    alert(
        "Patient Delete selected: " + id
    );

}


function editDoctor(id) {

    alert(
        "Doctor Edit selected: " + id
    );

}


function deleteDoctor(id) {

    alert(
        "Doctor Delete selected: " + id
    );

}


function editAppointment(id) {

    alert(
        "Appointment Edit selected: " + id
    );

}


function deleteAppointment(id) {

    alert(
        "Appointment Delete selected: " + id
    );

}


// ==========================================
// START FRONTEND AND CONNECT TO BACKEND
// ==========================================

document.addEventListener(
    "DOMContentLoaded",
    function () {

        console.log(
            "HTML loaded. Connecting to backend..."
        );


        loadPatients();

        loadDoctors();

        loadAppointments();

    }
);