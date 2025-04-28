<script setup>
import axios from "axios";
import { onMounted, ref } from "vue";

const items = ref([])

onMounted(async () => {
  axios.defaults.baseURL = "https://localhost:7117/"

  await axios.get("asteroids").then(response => items.value = response.data)
})

</script>

<template>
  <table>
    <tr>
      <th>Год</th>
      <th>Количество</th>
      <th>Суммарная масса</th>
    </tr>
    <tr v-for="item in items" :key="item.year">
      <td>{{ item.year }}</td>
      <td>{{ item.quantity }}</td>
      <td>{{ item.sumMass }}</td>
    </tr>
  </table>
</template>
