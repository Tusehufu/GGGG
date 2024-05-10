<template>
    <label class="form-label">{{label}}</label>
    <div>
        <dx-slider :required="required"
                      :step="step"
                      :disabled="disabled"
                      :min="min"
                      :max="max"
                      v-model="computedValue">
        </dx-slider>
    </div>
</template>

<script setup lang="ts">
    import { computed } from 'vue';
    import { DxSlider } from 'devextreme-vue/slider';
    import { DxNumberBox } from 'devextreme-vue/number-box';

    defineOptions({
        inheritAttrs: false,
    });

    const props = defineProps({
        required: {
            type: Boolean,
        },
        step: {
            type: Number,
            default: 1,
        },
        disabled: {
            type: Boolean,
        },
        min: {
            type: Number,
            required: true,
        },
        max: {
            type: Number,
            required: true,
        },
        modelValue: {
            type: Number,
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

</script>