<script setup>
import { defineModel } from "vue"

const sortBy = defineModel("sortBy")
const sortDesc = defineModel("sortDesc")

import { NRadioGroup, NRadioButton, NSwitch, NIcon, NCard, NFlex } from "naive-ui"
import { TextSortAscending16Filled, TextSortDescending16Filled } from "@vicons/fluent"
</script>

<template>
    <n-card title="Sorting" style="flex:1;">
        <n-flex align="center" :wrap="false">
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
            <n-switch v-model:value="sortDesc" size="large">
                <template #checked-icon>
                    <n-icon :component="TextSortDescending16Filled" />
                </template>
                <template #unchecked-icon>
                    <n-icon :component="TextSortAscending16Filled" />
                </template>
            </n-switch>
        </n-flex>
    </n-card>
</template>