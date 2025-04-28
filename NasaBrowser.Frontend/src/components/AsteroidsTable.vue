<template>
    <div>
        <div v-if="error" class="error">{{ error }}</div>
        <div v-else>
            <div class="filters">
                <div class="filter-group">
                    <label>Start Year:</label>
                    <n-select clearable v-model:value="selectedStartYear" :options='sortedYears' />
                </div>

                <div class="filter-group">
                    <label>End Year:</label>
                    <n-select v-model:value="selectedEndYear" :options='sortedYears' />
                </div>

                <div class="filter-group">
                    <label>Class:</label>
                    <n-select v-model:value="selectedClass" :options='sortedClasses' />
                </div>

                <div class="filter-group">
                    <label>Name Contains:</label>
                    <input type="text" v-model="namePart" @input="handleNameInput" />
                </div>

                <div class="filter-group">
                    <label>Sort By:</label>
                    <n-select v-model:value="sortBy"
                        :options="[{ label: 'Year', value: 0 }, { label: 'Total Mass', value: 2 }, { label: 'Quantity', value: 1 }]" />
                </div>

                <div class="filter-group">
                    <label>
                        <input type="checkbox" v-model="sortDesc" />
                        Descending
                    </label>
                </div>
            </div>
            <n-table v-if="asteroidsData.length" :bordered="false" :single-line="false">
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
                        <td>{{ item.sumMass.toLocaleString() }}</td>
                        <td>{{ item.quantity }}</td>
                    </tr>
                </tbody>
            </n-table>
            <div v-else>No data found</div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, computed, watch } from 'vue';
import { fetchAsteroids, fetchYears, fetchClasses } from '@/services/api';
import { NTable, NSelect } from "naive-ui"

const yearsList = ref([]);
const classesList = ref([]);
const selectedStartYear = ref(null);
const selectedEndYear = ref(null);
const selectedClass = ref('');
const namePart = ref('');
const sortBy = ref(0);
const sortDesc = ref(true);
const asteroidsData = ref([]);
const error = ref(null);
const loading = ref(false);

const sortedYears = computed(() =>
    yearsList.value.map(item => { return { label: item, value: item } }).sort((a, b) => a.value > b.value)
);

const sortedClasses = computed(() =>
    classesList.value.map(item => { return { label: item, value: item } }).sort((a, b) => a.value > b.value)
);

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
            fetchClasses()
        ]);

        yearsList.value = years;
        classesList.value = classes;

        if (years.length) {
            selectedStartYear.value = Math.min(...years);
            selectedEndYear.value = Math.max(...years);
        }
    } catch (err) {
        error.value = err.message;
    }
});

watch(
    [selectedStartYear, selectedEndYear, selectedClass, sortBy, sortDesc],
    loadAsteroids
);

const handleNameInput = debounce(loadAsteroids, 500);

function debounce(fn, delay) {
    let timeout;
    return (...args) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => fn.apply(this, args), delay);
    };
}
</script>

<style scoped>
.filters {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
    gap: 1rem;
    margin-bottom: 2rem;
}

.filter-group {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}

.error {
    color: red;
    padding: 1rem;
    border: 1px solid red;
}
</style>