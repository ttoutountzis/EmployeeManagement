import React, { useEffect, useState } from "react";
import axios from "../../src/app/utils/axios";
import {
  Container,
  TextField,
  Button,
  Typography,
  Box,
  Paper,
} from "@mui/material";
import Select from "react-select";

const NewEmployee = () => {
  const [employee, setEmployee] = useState({
    firstName: "",
    lastName: "",
    hireDate: "",
    skillIds: [],
  });
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    axios.get("/skills").then((response) => {
      setSkills(
        response.data.map((skill) => ({ value: skill.id, label: skill.name }))
      );
    });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post("/employee", {
        firstName: employee.firstName,
        lastName: employee.lastName,
        hireDate: employee.hireDate,
        skillIds: employee.skillIds.map((skill) => skill.value),
      });
      alert("Employee created successfully!");
    } catch (error) {
      console.error("Error creating employee:", error);
    }
  };

  const handleFileUpload = async (e) => {
    const file = e.target.files[0];
    const formData = new FormData();
    formData.append("file", file);
    try {
      await axios.post("/employee/import", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      alert("Employees imported successfully!");
    } catch (error) {
      console.error("Error importing employees:", error);
    }
  };

  return (
    <Container>
      <Box my={4} component={Paper} p={3}>
        <Typography variant="h4" gutterBottom>
          Create New Employee
        </Typography>
        <Box mb={2}>
          <Typography>Import Employees from CSV</Typography>
          <input type="file" accept=".csv" onChange={handleFileUpload} />
        </Box>
        <form onSubmit={handleSubmit}>
          <TextField
            label="First Name"
            variant="outlined"
            fullWidth
            margin="normal"
            value={employee.firstName}
            onChange={(e) =>
              setEmployee({ ...employee, firstName: e.target.value })
            }
            required
          />
          <TextField
            label="Last Name"
            variant="outlined"
            fullWidth
            margin="normal"
            value={employee.lastName}
            onChange={(e) =>
              setEmployee({ ...employee, lastName: e.target.value })
            }
            required
          />
          <TextField
            label="Hire Date"
            type="date"
            InputLabelProps={{ shrink: true }}
            variant="outlined"
            fullWidth
            margin="normal"
            value={employee.hireDate}
            onChange={(e) =>
              setEmployee({ ...employee, hireDate: e.target.value })
            }
            required
          />
          <Box my={2}>
            <Typography>Skills</Typography>
            <Select
              isMulti
              options={skills}
              onChange={(selectedOptions) =>
                setEmployee({ ...employee, skillIds: selectedOptions })
              }
            />
          </Box>
          <Button variant="contained" color="primary" type="submit">
            Create
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default NewEmployee;
