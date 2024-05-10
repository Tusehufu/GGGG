<template>
    <fieldset  :disabled="props.disabled">
        <div class="input-group" :class="{ [`input-group-${props.size}`]: props.size}">
            <slot name="prepend" />

            <slot />
            <slot name="append" />
        </div>
    </fieldset>

</template>
<script setup lang="ts">
    import { computed } from 'vue';


    const props = defineProps({
        size: {
            type: String,
            default: null,
            validator: (value: string) => {
                const allowedSizes = ['sm', 'lg'];

                if (allowedSizes.includes(value) || value === null)
                    return true;

                throw new Error(`Ogiltig 'size': ${value}`);
            },
        },
        disabled: {
            type: Boolean,
            default: true,
        },
    });
</script>