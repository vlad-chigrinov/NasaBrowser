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
                    <n-input v-model:value="namePart" type="text" placeholder="Basic Input" @input="handleNameInput" />
                </div>

                <div class="filter-group">
                    <label>Sort By:</label>
                    <n-radio-group v-model:value="sortBy">
                        <n-radio-button :value="Number(0)">
                            Year
                        </n-radio-button>
                        <n-radio-button :value="Number(2)">
                            Total Mass
                        </n-radio-button>
                        <n-radio-button :value="Number(1)">
                            Quantity
                        </n-radio-button>
                    </n-radio-group>
                </div>

                <n-switch v-model:value="sortDesc" size="large">
                    <template #checked-icon>
                        <n-icon :component="TextSortAscending16Filled" />
                    </template>
                    <template #unchecked-icon>
                        <n-icon :component="TextSortDescending16Filled" />
                    </template>
                </n-switch>
            </div>
            <n-alert v-if="error" title="Internal Error" type="error">
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
            <n-alert v-else title="Empty list" type="wan">
                
            </n-alert>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, computed, watch } from 'vue';
import { fetchAsteroids, fetchYears, fetchClasses } from '@/services/api';
import { NTable, NRadioGroup, NRadioButton, NSwitch, NIcon, NInput, NSelect, NAlert } from "naive-ui"
import { TextSortAscending16Filled, TextSortDescending16Filled } from "@vicons/fluent"

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
    color: rgb(255, 0, 0);
    padding: 1rem;
    border: 1px solid rgb(255, 0, 0);
}
</style>