<template>
    <label v-if="label" class="form-label">{{ label }}</label>
    <div>
        <kendo-color-picker :required="required"
                            :disabled="disabled"
                            :value="computedValue"
                            @change="handleChange">
        </kendo-color-picker>
        {{computedValue}}
    </div>
</template>

<script setup lang="ts">
    import {
        computed,
    } from 'vue';
    import { ColorPickerChangeEvent, ColorPicker as KendoColorPicker } from '@progress/kendo-vue-inputs';

    defineOptions({
        inheritAttrs: false
    });

    const props = defineProps({
        required: {
            type: Boolean,
        },
        disabled: {
            type: Boolean,
        },
        label: {
            type: String,
        },
        modelValue: {
            type: String,
            required: true,
        },
    });

    const emit = defineEmits(['update:modelValue']);

    const computedValue = computed({
        get: () => props.modelValue,
        set: (newValue) => {
            emit('update:modelValue', newValue);
       }
    });

    const handleChange = (event: ColorPickerChangeEvent) => {
        debugger
        computedValue.value = event.event.value;
    };
</script>
