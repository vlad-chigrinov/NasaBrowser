<script setup>
import { NInput, NSelect, NCard, NFlex } from "naive-ui"
import { defineModel, computed } from "vue"

const selectedStartYear = defineModel("selectedStartYear")
const selectedEndYear = defineModel("selectedEndYear")
const selectedClass = defineModel("selectedClass")
const namePart = defineModel("namePart")

const emit = defineEmits(['onInputNamePart'])

const { years, classes } = defineProps(['years', 'classes'])

const handleNameInput = debounce(()=>emit('onInputNamePart'), 500);

function debounce(fn, delay) {
    let timeout;
    return (...args) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => fn.apply(this, args), delay);
    };
}

const sortedYears = computed(() =>
    years
        .map(item => { return { label: item, value: item } })
);

const sortedClasses = computed(() =>
    classes
        .map(item => { return { label: item, value: item } })
);

</script>

<template>
    <n-card title="Filters" style="flex:2.5;">
        <n-flex justify="space-between" align="center" class="filters" :wrap="false">
            <div class="filter-group">
                <label>Start Year:</label>
                <n-select clearable v-model:value="selectedStartYear" :options='sortedYears'
                    placeholder="Select Year..." />
            </div>

            <div class="filter-group">
                <label>End Year:</label>
                <n-select clearable v-model:value="selectedEndYear" :options='sortedYears'
                    placeholder="Select Year..." />
            </div>

            <div class="filter-group">
                <label>Class:</label>
                <n-select clearable v-model:value="selectedClass" :options='sortedClasses'
                    placeholder="Select RecClass..." />
            </div>

            <div class="filter-group">
                <label>Name Contains:</label>
                <n-input clearable v-model:value="namePart" type="text" @input="handleNameInput"
                    placeholder="Type a name part..." />
            </div>
        </n-flex>
    </n-card>
</template>

<style>
.filters>* {
    flex: 1;
}
</style>