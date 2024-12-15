import React, { useEffect, useState } from "react";
import { useRouter } from "next/router";
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

const UpdateEmployee = () => {
  const router = useRouter();
  const { id } = router.query;

  const [employee, setEmployee] = useState({
    firstName: "",
    lastName: "",
    hireDate: "",
    skillIds: [],
  });
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    if (id) {
      axios.get(`/employee/${id}`).then((response) => {
        setEmployee({
          firstName: response.data.firstName,
          lastName: response.data.lastName,
          hireDate: response.data.hireDate.split("T")[0],
          skillIds: response.data.skills.map((s) => ({
            value: s.id,
            label: s.name,
          })),
        });
      });
      axios.get("/skills").then((response) => {
        setSkills(
          response.data.map((skill) => ({ value: skill.id, label: skill.name }))
        );
      });
    }
  }, [id]);

  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`/employee/${id}`, {
        id: parseInt(id),
        firstName: employee.firstName,
        lastName: employee.lastName,
        hireDate: employee.hireDate,
        skillIds: employee.skillIds.map((s) => s.value),
      });
      alert("Employee updated successfully!");
      router.push(`/employees`);
    } catch (error) {
      console.error("Error updating employee:", error);
    }
  };

  return (
    <Container>
      <Box my={4} component={Paper} p={3}>
        <Typography variant="h4" gutterBottom>
          Update Employee
        </Typography>
        <form onSubmit={handleUpdate}>
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
              value={employee.skillIds}
              onChange={(selectedOptions) =>
                setEmployee({ ...employee, skillIds: selectedOptions })
              }
            />
          </Box>
          <Button variant="contained" color="primary" type="submit">
            Update
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default UpdateEmployee;
