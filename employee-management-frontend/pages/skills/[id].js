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

const SkillDetailsPage = () => {
  const router = useRouter();
  const { id } = router.query;
  const [skill, setSkill] = useState({ name: "", description: "" });

  useEffect(() => {
    if (id) {
      const fetchSkill = async () => {
        const response = await axios.get(`/skills/${id}`);
        setSkill(response.data);
      };
      fetchSkill();
    }
  }, [id]);

  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`/skills/${id}`, skill);
      alert("Skill updated successfully!");
    } catch (error) {
      console.error("Error updating skill:", error);
    }
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`/skills/${id}`);
      alert("Skill deleted successfully!");
      router.push("/skills");
    } catch (error) {
      console.error("Error deleting skill:", error);
    }
  };

  return (
    <Container>
      <Box my={4} component={Paper} p={3}>
        <Typography variant="h4" gutterBottom>
          Edit Skill
        </Typography>
        <form onSubmit={handleUpdate}>
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
          <Box display="flex" justifyContent="space-between" mt={2}>
            <Button variant="contained" color="primary" type="submit">
              Update
            </Button>
            <Button variant="outlined" color="secondary" onClick={handleDelete}>
              Delete
            </Button>
          </Box>
        </form>
      </Box>
    </Container>
  );
};

export default SkillDetailsPage;
