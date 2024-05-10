<template>
    <label class="form-label">{{label}}</label>
    <div>
        <kendo-slider :required="required"
                      :buttons="useButtons"
                      :step="step"
                      :default-value="defaultValue"
                      :disabled="disabled"
                      :min="min"
                      :max="max"
                      :value="computedValue"
                      @change="onChange">
            <slider-label v-for="labelValue in labelValues" :position="labelValue">{{labelValue}}</slider-label>
        </kendo-slider>
    </div>
</template>

<script setup lang="ts">
    import { computed } from 'vue';
    import {
        Slider as KendoSlider,
        SliderChangeEvent,
        SliderLabel,
    } from '@progress/kendo-vue-inputs';

    defineOptions({
        inheritAttrs: false
    });

    const props = defineProps({
        required: {
            type: Boolean,
        },
        useButtons: {
            type: Boolean,
            default: true,
        },
        step: {
            type: Number,
            default: 1,
        },
        defaultValue: {
            type: Number,
            required: false,
        },
        disabled: {
            type: Boolean,
        },
        min: {
            type: Number,
            required: true
        },
        max: {
            type: Number,
            required: true
        },
        modelValue: {
            type: Number,
            required: true,
        },
        labelValues: {
            type: Array as () => Array<number>,
            required: true,
        },
        label: {
            type: String,
            default: '',
        },
    });

    const emit = defineEmits(['update:modelValue']);

    const computedValue = computed({
        get: () => props.modelValue,
        set: (newValue) => {
            emit('update:modelValue', newValue);
        }
    });

    const onChange = (event: SliderChangeEvent) => {
        const roundedValue = Math.round(event.value);
        emit('update:modelValue', roundedValue);
    };
</script>