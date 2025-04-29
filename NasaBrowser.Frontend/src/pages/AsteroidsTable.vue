<template>
    <n-spin :show="loading">
        <n-flex class="cards-flex">
            <sort-by-card v-model:sortBy="sortBy" v-model:sortDesc="sortDesc" />
            <filters-card v-model:selectedStartYear="selectedStartYear" v-model:selectedEndYear="selectedEndYear"
                v-model:selectedClass="selectedClass" v-model:namePart="namePart" @onInputNamePart="loadAsteroids"
                :years="yearsList" :classes="classesList" />
        </n-flex>
        <n-alert v-if="error" title="Applicatoin Error" type="error">
            {{ error }}
        </n-alert>
        <n-table v-else-if="asteroidsData.length" :bordered="false" :single-line="false">
            <thead>
                <tr>
                    <th>Year</th>
                    <th>Total Mass</th>
                    <th>Quantity</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in asteroidsData" :key="item.year">
                    <td>{{ item.year }}</td>
                    <td>{{ item.sumMass }}</td>
                    <td>{{ item.quantity }}</td>
                </tr>
            </tbody>
        </n-table>
        <n-alert v-else title="Empty list" type="warning">
            Items Not Found
        </n-alert>
    </n-spin>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import { fetchAsteroids, fetchYears, fetchClasses } from '@/services/api';
import { NTable, NAlert, NSpin, NFlex } from "naive-ui"
import SortByCard from '@/components/SortByCard.vue';
import FiltersCard from '@/components/FiltersCard.vue';

const yearsList = ref([]);
const classesList = ref([]);

const selectedStartYear = ref(null);
const selectedEndYear = ref(null);
const selectedClass = ref(null);
const namePart = ref('');
const sortBy = ref(0);
const sortDesc = ref(false);

const asteroidsData = ref([]);

const error = ref(null);
const loading = ref(false);

const loadAsteroids = async () => {
    loading.value = true;
    try {
        const params = {
            StartYear: selectedStartYear.value,
            EndYear: selectedEndYear.value,
            RecClass: selectedClass.value,
            NamePart: namePart.value,
            SortBy: sortBy.value,
            Desc: sortDesc.value
        };

        const data = await fetchAsteroids(params);
        asteroidsData.value = data;
        error.value = null;
    } catch (err) {
        error.value = err.message;
        asteroidsData.value = [];
    } finally {
        loading.value = false;
    }
};

onMounted(async () => {
    try {
        const [years, classes] = await Promise.all([
            fetchYears(),
            fetchClasses(),
            loadAsteroids()
        ]);

        yearsList.value = years;
        classesList.value = classes;

    } catch (err) {
        error.value = err.message;
    }
});

watch(
    [selectedStartYear, selectedEndYear, selectedClass, sortBy, sortDesc],
    loadAsteroids
);

</script>
<style>
@media(max-width: 900px) {
    .cards-flex {
        flex-direction: column !important;
    }
}
</style>