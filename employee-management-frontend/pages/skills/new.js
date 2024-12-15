import React, { useState } from "react";
import axios from "../../src/app/utils/axios";
import {
  Container,
  TextField,
  Button,
  Typography,
  Box,
  Paper,
} from "@mui/material";

const NewSkillPage = () => {
  const [skill, setSkill] = useState({ name: "", description: "" });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post("/skills", skill);
      alert("Skill created successfully!");
      setSkill({ name: "", description: "" });
    } catch (error) {
      console.error("Error creating skill:", error);
    }
  };

  return (
    <Container>
      <Box my={4} component={Paper} p={3}>
        <Typography variant="h4" gutterBottom>
          Create New Skill
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Name"
            variant="outlined"
            fullWidth
            margin="normal"
            value={skill.name}
            onChange={(e) => setSkill({ ...skill, name: e.target.value })}
            required
          />
          <TextField
            label="Description"
            variant="outlined"
            fullWidth
            margin="normal"
            value={skill.description}
            onChange={(e) => setSkill({ ...skill, description: e.target.value })}
            required
          />
          <Button variant="contained" color="primary" type="submit" style={{ marginTop: "10px" }}>
            Create
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default NewSkillPage;
