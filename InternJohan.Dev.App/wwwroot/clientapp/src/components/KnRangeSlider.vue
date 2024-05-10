<template>
    <label class="form-label">{{label}}</label>
    <div>
        <kendo-range-slider :required="required"
                            :step="step"
                            :default-value=" {start: internalValue.start, end: internalValue.end} "
                            :disabled="disabled"
                            :min="min"
                            :max="max"
                            :value="internalValue"
                            @change="onChange">
            <KendoSliderLabel v-for="labelValue in labelValues" :position="labelValue" :key="labelValue">{{ labelValue }}</KendoSliderLabel>
        </kendo-range-slider>
    </div>
</template>

<script setup lang="ts">
    import {
        defineProps, defineEmits,
        ref, PropType
    } from 'vue';
    import {
        RangeSlider as KendoRangeSlider,
        SliderChangeEvent,
        SliderLabel as KendoSliderLabel,
        RangeSliderChangeEvent
    } from '@progress/kendo-vue-inputs';
    import RangeSliderValue from '../types/RangeSliderValue';

    defineOptions({
        inheritAttrs: false
    });

    const props = defineProps({
        required: {
            type: Boolean,
        },
        step: {
            type: Number,
            default: 1,
        },
        value: {
            type: Object as PropType<RangeSliderValue>,
            required: true,
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
        labelValues: {
            type: Array as () => Array<number>,
            required: true,
        },
        label: {
            type: String,
            default: '',
        },
    });

    const emit = defineEmits(['change']);

    const internalValue = ref({
        start: props.value.start,
        end: props.value.end
    });

    const onChange = (event: RangeSliderChangeEvent) => {
        const roundedStart = Math.round(event.value.start);
        const roundedEnd = Math.round(event.value.end);

        internalValue.value = {
            start: roundedStart,
            end: roundedEnd
        };
        emit('change', internalValue.value);
    };
</script>